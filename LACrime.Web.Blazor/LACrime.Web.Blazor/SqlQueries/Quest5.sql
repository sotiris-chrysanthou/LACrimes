SELECT 
    crm.code AS CrmCd,
    COUNT(*) AS TotalReports
FROM
    crimes crm
INNER JOIN crimeseverities crms ON crms.crimeid = crm.id
INNER JOIN crimesrecords crmr ON crmr.id = crms.crimerecordid
INNER JOIN coordinates coord ON coord.id = crmr.coordinatesid
WHERE
    crmr.dateocc = @date
    AND coord.lat BETWEEN @minLat AND @maxLat
    AND coord.lon BETWEEN @minLon AND @maxLon
GROUP BY crm.code
ORDER BY TotalReports DESC
LIMIT 1;