WITH CrimeWeaponPairs AS (
    SELECT
        crmr.dateocc AS CrimeDate,
        crms.crimeid AS CrimeID,
        crmr.weaponid AS WeaponID,
        sa.areaid AS AreaID,
        crmr.drno AS CrimeDrNo
    FROM
        crimesrecords crmr
    INNER JOIN crimeseverities crms ON crms.crimerecordid = crmr.id
    INNER JOIN subareas sa ON sa.id = crmr.subareaid
    WHERE
        crmr.dateocc + crmr.timeocc BETWEEN @startDate AND @endDate
),
SameDayDifferentArea AS (
    SELECT
        cwp1.CrimeDrNo
    FROM
        CrimeWeaponPairs cwp1
    INNER JOIN CrimeWeaponPairs cwp2 ON
        cwp1.CrimeDate = cwp2.CrimeDate AND
        cwp1.CrimeID = cwp2.CrimeID AND
        cwp1.WeaponID = cwp2.WeaponID AND
        cwp1.AreaID <> cwp2.AreaID
)
SELECT
    DISTINCT cwp1.CrimeDrNo AS DrNo
FROM
    SameDayDifferentArea cwp1