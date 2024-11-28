SELECT 
    crmr.dateocc::date AS ReportDate, COUNT(*) AS TotalReports
FROM
    crimes crm
INNER JOIN crimeseverities crms ON crms.crimeid = crm.id
INNER JOIN crimesrecords crmr ON crmr.id = crms.crimerecordid
WHERE
    crm.code = @crmCd AND crmr.dateocc >= @startDate AND crmr.dateocc <= @endDate
GROUP BY crmr.dateocc
ORDER BY crmr.dateocc