SELECT 
	crm1.code AS Crime1Code,
	crm1."desc" AS Crime1Desc,
	crm2.code AS Crime2Code,
	crm2."desc" AS Crime2Desc,
	COUNT(*) as CrimeCount
FROM
	crimesrecords crmr
INNER JOIN crimeseverities crms1 ON crms1.crimerecordid = crmr.id
INNER JOIN crimeseverities crms2 ON crms1.crimerecordid = crms2.crimerecordid AND crms1.id <> crms2.id
INNER JOIN crimes crm1 on crm1.id = crms1.crimeid
INNER JOIN crimes crm2 on crm2.id = crms2.crimeid
WHERE
	crm1.code = @code
	AND crmr.dateocc BETWEEN @startdate AND @enddate
GROUP BY 
	Crime1Code,
	Crime1Desc,
	Crime2Code,
	Crime2Desc
ORDER BY CrimeCount DESC
OFFSET 1 LIMIT 1