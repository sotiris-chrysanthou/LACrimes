SELECT
    sa.rpddistno AS RptDistNo,
    crmr.dateocc AS DateOccurred,
    COUNT(*) AS TotalCrimes
FROM
    crimesrecords crmr
INNER JOIN subareas sa on sa.id = crmr.subareaid
WHERE
    crmr.dateocc BETWEEN @startDate AND @endDate
GROUP BY
    sa.rpddistno,
    crmr.dateocc
ORDER BY
    TotalCrimes DESC
LIMIT 5;