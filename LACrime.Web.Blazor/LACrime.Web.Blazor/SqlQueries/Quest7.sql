WITH AreaIncidents AS (
    SELECT 
        a.id AS AreaID, 
        COUNT(*) AS TotalReports
    FROM 
        crimesrecords crmr
	INNER JOIN 
		subareas sa on sa.id = crmr.subareaid
	INNER JOIN 
		areas a on a.id = sa.areaid
    WHERE 
        crmr.dateocc BETWEEN @startdate AND @enddate
    GROUP BY 
        a.id
    ORDER BY 
        TotalReports DESC
    LIMIT 1
)
SELECT 
	a."name" As AreaName,
	c1.code AS Crime1,
	c1."desc" AS Crime1Desc,
	c2.code AS Crime2, 
	CASE
		WHEN c2."desc" IS NULL OR c2."desc" = ' ' THEN 'No description'
		ELSE c2."desc"
	END AS Crime2Desc,
	COUNT(*) AS CoOccurrences
FROM 
	crimeseverities crms1
INNER JOIN 
	crimeseverities crms2 ON crms1.crimerecordid = crms2.crimerecordid AND crms1.crimeid <> crms2.crimeid
INNER JOIN 
	crimesrecords crmr ON crmr.id = crms1.crimerecordid
INNER JOIN 
	subareas sa on sa.id = crmr.subareaid
INNER JOIN 
	areas a on a.id = sa.areaid
INNER JOIN 
	crimes c1 ON c1.id = crms1.crimeid
INNER JOIN 
	crimes c2 ON c2.id = crms2.crimeid
WHERE
	crms1.severity = 1
	AND
	a.id = (SELECT areaid FROM AreaIncidents)
	AND 
	crmr.dateocc BETWEEN @startdate AND @enddate
GROUP BY 
	a."name",
	c1.code,
	c1."desc",
	c2.code,
	c2."desc"
ORDER BY 
	CoOccurrences DESC
LIMIT 1