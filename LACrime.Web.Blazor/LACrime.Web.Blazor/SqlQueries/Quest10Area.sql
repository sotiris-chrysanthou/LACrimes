WITH crime_date_ranges AS (
    SELECT
        a.id AS AreaID,
        a.name AS AreaName,
        crmr.dateocc AS CrimeDate,
		LAG(crmr.dateocc) OVER (PARTITION BY AreaID ORDER BY crmr.dateocc ) AS PrevCrimeDate
    FROM
        crimesrecords crmr
    INNER JOIN crimeseverities cs ON cs.crimerecordid = crmr.id
    INNER JOIN crimes c ON c.id = cs.crimeid
    INNER JOIN subareas sa ON sa.id = crmr.subareaid
    INNER JOIN areas a ON a.id = sa.areaid
    WHERE
        c.code = @code
),
gaps AS (
    SELECT
        AreaID,
        AreaName,
		PrevCrimeDate,
        CrimeDate AS CurrentCrimeDate,
        EXTRACT(DAY FROM (CrimeDate - PrevCrimeDate))  AS GapDays
    FROM
        crime_date_ranges
    WHERE
        PrevCrimeDate IS NOT NULL
 ),
max_gap_per_area AS (
    SELECT
        AreaID,
        AreaName,
        MAX(GapDays) AS MaxGapDays
    FROM
        gaps
    GROUP BY
        AreaID,
        AreaName
 ),
max_gap_details AS (
    SELECT
        g.AreaID,
        g.AreaName,
        g.PrevCrimeDate AS StartDate,
        g.CurrentCrimeDate AS EndDate,
        g.GapDays
    FROM
        gaps g
    INNER JOIN max_gap_per_area m ON
        g.AreaID = m.AreaID AND g.GapDays = m.MaxGapDays
 )
SELECT
    AreaName,
    StartDate,
    EndDate,
    GapDays AS MaxGapDays
FROM
    max_gap_details
ORDER BY
    MaxGapDays DESC
LIMIT 1;
