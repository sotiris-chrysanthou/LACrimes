WITH crime_counts AS (
    SELECT
        a.name AS AreaName,
        cr.dateocc AS CrimeDate,
        c1.code AS Crime1Code,
        c2.code AS Crime2Code,
        COUNT(*) AS CrimesCount
    FROM
        crimesrecords cr
    INNER JOIN crimeseverities cs1 ON cs1.crimerecordid = cr.id
    INNER JOIN crimes c1 ON c1.id = cs1.crimeid
    INNER JOIN crimeseverities cs2 ON cs2.crimerecordid = cr.id AND cs2.id <> cs1.id
    INNER JOIN crimes c2 ON c2.id = cs2.crimeid
    INNER JOIN subareas sa ON sa.id = cr.subareaid
    INNER JOIN areas a ON a.id = sa.areaid
    WHERE
	    c1.code = @code1 AND c2.code = @code2
    GROUP BY
        sa.areaid, a.name, cr.dateocc, c1.code, c2.code
    )
SELECT
    AreaName,
    CrimeDate,
    Crime1Code,
    Crime2Code,
    CrimesCount
FROM
    crime_counts
WHERE
    CrimesCount > 1
ORDER BY
    AreaName, CrimeDate;