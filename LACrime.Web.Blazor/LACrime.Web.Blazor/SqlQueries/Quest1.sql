SELECT 
    crm.code AS CrmCd, 
    crm."desc" AS CrimeDescription, 
    COUNT(crm.code) AS TotalReports
FROM
    crimes crm
INNER JOIN crimeseverities crms ON crms.crimeid = crm.id
INNER JOIN crimesrecords crmr ON crmr.id = crms.crimerecordid
WHERE
    crmr.dateocc BETWEEN @startdate AND @enddate
    AND crms.severity = @severity
GROUP BY crm.code, crm."desc"
ORDER BY TotalReports DESC