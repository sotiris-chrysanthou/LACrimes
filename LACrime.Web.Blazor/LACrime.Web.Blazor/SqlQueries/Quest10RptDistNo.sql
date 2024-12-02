WITH crime_date_ranges AS (
    SELECT
        sa.id AS SubAreaID,
        sa.rpddistno AS RptDistNo,
        crmr.dateocc AS CrimeDate,
		LAG(crmr.dateocc) OVER (PARTITION BY SubAreaID ORDER BY crmr.dateocc ) AS PrevCrimeDate
    FROM
        crimesrecords crmr
    INNER JOIN crimeseverities cs ON cs.crimerecordid = crmr.id
    INNER JOIN crimes c ON c.id = cs.crimeid
    INNER JOIN subareas sa ON sa.id = crmr.subareaid
    WHERE
        c.code = @code
),
gaps AS (
    SELECT
        RptDistNo,
        PrevCrimeDate,
        CrimeDate AS CurrentCrimeDate,
        EXTRACT(EPOCH FROM (CrimeDate - PrevCrimeDate)) / 86400 AS GapDays
    FROM
        crime_date_ranges
    WHERE
        PrevCrimeDate IS NOT NULL
),
max_gap_per_rptdistno AS (
    SELECT
        RptDistNo,
        MAX(GapDays) AS MaxGapDays
    FROM
        gaps
    GROUP BY
        RptDistNo
),
max_gap_details AS (
    SELECT
        g.RptDistNo,
        g.PrevCrimeDate AS StartDate,
        g.CurrentCrimeDate AS EndDate,
        g.GapDays
    FROM
        gaps g
    INNER JOIN max_gap_per_rptdistno m ON
        g.RptDistNo = m.RptDistNo AND g.GapDays = m.MaxGapDays
)
SELECT
    RptDistNo,
    StartDate,
    EndDate,
    GapDays AS MaxGapDays
FROM
    max_gap_details
ORDER BY
    MaxGapDays DESC
LIMIT 1;

