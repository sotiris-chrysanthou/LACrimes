SELECT
    ar.name AS AreaName,
    crmr.dateocc AS DateOccurred,
    COUNT(*) AS TotalCrimes
FROM
    crimesrecords crmr
INNER JOIN subareas sa ON sa.id = crmr.subareaid
INNER JOIN areas ar ON ar.id = sa.areaid
WHERE
    crmr.dateocc BETWEEN @startDate AND @endDate
GROUP BY
    ar.name,
    crmr.dateocc
ORDER BY
    TotalCrimes DESC
LIMIT 5;