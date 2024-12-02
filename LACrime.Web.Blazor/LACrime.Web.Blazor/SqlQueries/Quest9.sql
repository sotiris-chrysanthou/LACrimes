WITH RankedWeapons AS (
    SELECT
        (v.age / 5) * 5 AS AgeGroupStart,
		w.code AS WeaponCode,
        w."desc" AS WeaponDescription,
		COUNT(*) AS Count,
		ROW_NUMBER() OVER (PARTITION BY (v.age / 5) * 5 ORDER BY COUNT(*) DESC) AS Rank
    FROM
        victims v
    INNER JOIN
        crimesrecords crmr ON crmr.victimid = v.id
	INNER JOIN
        weapons w ON w.id = crmr.weaponid
    WHERE
        v.age IS NOT NULL AND crmr.weaponid IS NOT NULL
	GROUP BY
        (v.age / 5) * 5,
		w.code,
        w."desc"
)
SELECT
	AgeGroupStart,
    AgeGroupStart::text || '-' || (AgeGroupStart + 4)::text AS AgeGroup,
    WeaponCode,
    WeaponDescription,
    Count
FROM
    RankedWeapons
WHERE
    Rank = 1
ORDER BY
    AgeGroup;