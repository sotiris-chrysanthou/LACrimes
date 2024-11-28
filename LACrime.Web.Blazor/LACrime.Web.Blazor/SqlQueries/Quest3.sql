SELECT 
    a.code AS AreaCode,
    a.name AS AreaName, 
    crm.code AS CrimeCode,
    crm."desc" AS CrimeDescription,
    COUNT(*) AS TotalReports
FROM
    crimes crm
INNER JOIN crimeseverities crms ON crms.crimeid = crm.id
INNER JOIN crimesrecords crmr ON crmr.id = crms.crimerecordid
INNER JOIN subareas sa ON sa.id = crmr.subareaid
INNER JOIN areas a ON a.id = sa.areaid
WHERE
    crmr.dateocc::date = @date AND a.code NOT IN ('01', '02', '03', '04')
GROUP BY a.code, a.name, crm.code, crm."desc"
ORDER BY a.name, TotalReports DESC