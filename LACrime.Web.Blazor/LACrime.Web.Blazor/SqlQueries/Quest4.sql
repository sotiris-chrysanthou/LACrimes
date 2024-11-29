SELECT 
    hour,
    AVG(count) AS AverageCrimes
FROM
    (SELECT 
        EXTRACT(HOUR FROM crmr.timeocc) AS hour,
        COUNT(*) AS count
    FROM
        crimes crm
    INNER JOIN crimeseverities crms ON crms.crimeid = crm.id
    INNER JOIN crimesrecords crmr ON crmr.id = crms.crimerecordid
    WHERE
        crmr.dateocc >= @startDate AND crmr.dateocc <= @endDate
    GROUP BY EXTRACT(HOUR FROM crmr.timeocc), crmr.dateocc) AS hourly_crimes
GROUP BY hour
ORDER BY hour;