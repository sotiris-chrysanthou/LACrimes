WITH CrimeOccurrences AS (
    SELECT
        crmr.dateocc AS CrimeDate,
        a."name" AS AreaName,
		w.code AS WeaponCode,
        w."desc" AS WeaponDesc,
		c.code AS CrimeCode,
        c."desc" AS CrimeDesc,
        crmr.drno AS CrimeDrNo
    FROM
        crimesrecords crmr
    INNER JOIN crimeseverities crms ON crms.crimerecordid = crmr.id
    INNER JOIN crimes c ON c.id = crms.crimeid
    INNER JOIN weapons w ON w.id = crmr.weaponid
    INNER JOIN subareas sa ON sa.id = crmr.subareaid
    INNER JOIN areas a ON a.id = sa.areaid
    WHERE
        crmr.dateocc + crmr.timeocc BETWEEN @startDate AND @endDate
)
SELECT
	CrimeDate,
	AreaName,
	WeaponCode,
	WeaponDesc,
	CrimeCode,
	CrimeDesc,
	STRING_AGG(CrimeDrNo, ', ') AS ListOfDrNo
FROM
	CrimeOccurrences
GROUP BY
	CrimeDate,
	AreaName,
	WeaponCode,
	WeaponDesc,
	CrimeCode,
	CrimeDesc
HAVING COUNT(*) = @N