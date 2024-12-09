# M149 Database Management Systems - Fall'24  
## Programming Assignment 1  

**Instructor:** Alex Delis  
**Creator Name:** Sotiris Chrysanthou  
**ID:** 7115112400017  

---

## 📄 **Table of Contents**
1. [Project Implementation Summary](#project-implementation-summary)  
2. [Summarized Database Schema](#summarized-database-schema)  
   - [accounts table](#accounts)  
   - [areas table](#areas)  
   - [coordinates table](#coordinates)  
   - [crimes table](#crimes)  
   - [crimeseverities table](#crimeseverities)  
   - [crimesrecords table](#crimesrecords)  
   - [premis table](#premis)  
   - [statuses table](#statuses)  
   - [streets table](#streets)  
   - [subareas table](#subareas)  
   - [victims table](#victims)  
   - [weapons table](#weapons)  
3. [SQL Queries](#sql-queries)
   - [Query 1](#Query-1) 
   - [Query 2](#Query-2) 
   - [Query 3](#Query-3) 
   - [Query 4](#Query-4) 
   - [Query 5](#Query-5) 
   - [Query 6](#Query-6) 
   - [Query 7](#Query-7) 
   - [Query 8](#Query-8) 
   - [Query 9](#Query-9) 
   - [Query 10](#Query-10) 
   - [Query 11](#Query-11) 
   - [Query 12](#Query-12) 
   - [Query 13](#Query-13) 
5. [Screen Shots](#screen-shots)

---

## 📖 **Project Implementation Summary**

This project is based on a previous personal project developed during the **Epsilon Net Coding School course**.  
[Link to the original repository](https://github.com/sotirischrysanthou/Coding-School-2023).

The project fulfills the following objectives:
- **Database Design & Implementation:**  
  Using **.NET 9 Entity Framework** with PostgreSQL. A fully normalized schema was created.
- **SQL Queries:**  
  Implementation & testing of various aggregation, filtering, and trend analysis queries.
- **Web Application:**  
  Features include user registration, login authentication, data search/edit, and query execution.
- **Performance Optimization:**  
  Indexing was added to frequently searched fields, and cascading behavior (`ON DELETE CASCADE`) was implemented.
- **Documentation:**  
  Includes design justifications, code, snapshots of the UI, and all required final information.

---

## 📊 **Summarized Database Schema**

### **accounts**
- **Fields:**  
  - `id (UUID, Primary Key)`  
  - `username (String, 20)`  
  - `password (String, 20)`  
  - `role (Text)`

### **areas**
- **Fields:**  
  - `id (UUID, Primary Key)`  
  - `code (Text)`  
  - `name (String, 40)`

### **coordinates**
- **Fields:**  
  - `id (UUID, Primary Key)`  
  - `lat (Double Precision)`  
  - `lon (Double Precision)`

### **crimes**
- **Fields:**  
  - `id (UUID, Primary Key)`  
  - `code (Integer)`  
  - `desc (String, 100)`

### **crimeseverities**
- **Fields:**  
  - `id (UUID, Primary Key)`  
  - `severity (Integer)`  
  - `crimeid (UUID, Foreign Key to crimes, Indexed)`  
  - `crimerecordid (UUID, Foreign Key to crimesrecords, Indexed)`

### **crimesrecords**
- **Fields:**  
  - `id (UUID, Primary Key)`  
  - `drno (Text)`  
  - `daterptd (Timestamp)`  
  - `dateocc (Timestamp)`  
  - `timeocc (Time)`  
  - `subareaid (UUID, Foreign Key to subareas, Indexed)`  
  - `victimid (UUID, Foreign Key to victims, Indexed)`  
  - `premisid (UUID, Foreign Key to premis, Indexed)`  
  - `statusid (UUID, Foreign Key to statuses, Indexed)`  
  - `weaponid (UUID, Foreign Key to weapons, Indexed)`  
  - `streetid (UUID, Foreign Key to streets, Indexed)`  
  - `crossstreetid (UUID, Foreign Key to streets, Indexed)`  
  - `coordinatesid (UUID, Foreign Key to coordinates, Indexed)`

### **premis**
- **Fields:**  
  - `id (UUID, Primary Key)`  
  - `code (Integer)`  
  - `desc (String, 100)`

### **statuses**
- **Fields:**  
  - `id (UUID, Primary Key)`  
  - `code (String, 2)`  
  - `desc (String, 20)`

### **streets**
- **Fields:**  
  - `id (UUID, Primary Key)`  
  - `name (String, 100)`

### **subareas**
- **Fields:**  
  - `id (UUID, Primary Key)`  
  - `rpddistno (Text)`  
  - `areaid (UUID, Foreign Key to areas, Indexed)`

### **victims**
- **Fields:**  
  - `id (UUID, Primary Key)`  
  - `age (Integer)`  
  - `sex (String, 1)`  
  - `descent (String, 1)`

### **weapons**
- **Fields:**  
  - `id (UUID, Primary Key)`  
  - `code (Integer)`  
  - `desc (String, 100)`

---

## 🛠️ **SQL Queries**

### **Query 1**: Find the total number of reports per “Crm Cd” that occurred within a specified time range and sort them in descending order
```sql
SELECT 
    crm.code AS CrmCd, 
    crm."desc" AS CrimeDescription, 
    COUNT(crm.code) AS TotalReports
FROM crimes crm
INNER JOIN crimeseverities crms ON crms.crimeid = crm.id
INNER JOIN crimesrecords crmr ON crmr.id = crms.crimerecordid
WHERE crmr.dateocc BETWEEN @startdate AND @enddate
    AND crms.severity = @severity
GROUP BY crm.code, crm."desc"
ORDER BY TotalReports DESC;
```

### **Query 2**: Find the total number of reports per day for a specific “Crm Cd” and time range.
```sql
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
```

### **Query 3**: Find the most common crime committed regardless of code 1, 2, 3, and 4, per area for a specific day.
```sql
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
```
### **Query 4**: Find the average number of crimes occurred per hour (24 hours) for a specific date range.
```sql
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
```
### **Query 5**: Find the mostcommon“Crm Cd”inaspecifiedboundingbox(asdesignated by GPS-coordinates) for a specific day.
```sql
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
```
### **Query 6**: Find the top-5 Area names with regards to total number of crimes reported per day for a specific date range. The same for Rpt Dist No.

### Top 5 areas
```sql
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
```

### Top 5 dpt dist no
```sql
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
```
### **Query 7**: Find the pair of crimes that has co-occurred in the area with the most reported incidents for a specific date range.
```sql
WITH AreaIncidents AS (
    SELECT 
        a.id AS AreaID, 
        COUNT(*) AS TotalReports
    FROM 
        crimesrecords crmr
	INNER JOIN 
		subareas sa on sa.id = crmr.subareaid
	INNER JOIN 
		areas a on a.id = sa.areaid
    WHERE 
        crmr.dateocc BETWEEN @startdate AND @enddate
    GROUP BY 
        a.id
    ORDER BY 
        TotalReports DESC
    LIMIT 1
)
SELECT 
	a."name" As AreaName,
	c1.code AS Crime1,
	c1."desc" AS Crime1Desc,
	c2.code AS Crime2, 
	CASE
		WHEN c2."desc" IS NULL OR c2."desc" = ' ' THEN 'No description'
		ELSE c2."desc"
	END AS Crime2Desc,
	COUNT(*) AS CoOccurrences
FROM 
	crimeseverities crms1
INNER JOIN 
	crimeseverities crms2 ON crms1.crimerecordid = crms2.crimerecordid AND crms1.crimeid <> crms2.crimeid
INNER JOIN 
	crimesrecords crmr ON crmr.id = crms1.crimerecordid
INNER JOIN 
	subareas sa on sa.id = crmr.subareaid
INNER JOIN 
	areas a on a.id = sa.areaid
INNER JOIN 
	crimes c1 ON c1.id = crms1.crimeid
INNER JOIN 
	crimes c2 ON c2.id = crms2.crimeid
WHERE
	crms1.severity = 1
	AND
	a.id = (SELECT areaid FROM AreaIncidents)
	AND 
	crmr.dateocc BETWEEN @startdate AND @enddate
GROUP BY 
	a."name",
	c1.code,
	c1."desc",
	c2.code,
	c2."desc"
ORDER BY 
	CoOccurrences DESC
LIMIT 1
```
### **Query 8**: Find the second most common crime that has co-occurred with a particular crime for a specifi date range.
```sql
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
```
### **Query 9**: Find the most common type of weapon used against victims depending on their group of age. The age groups are formed by bucketing ages every 5 years, e.g., 0 ≤ x < 5, 5 ≤ x < 10, etc..
```sql
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
```
### **Query 10**: Find the area with the longest time range without an occurrence of a specific crime. Include the time range in the results. The same for Rpt Dist No.
#### Area
```sql
WITH crime_date_ranges AS (
    SELECT
        a.id AS AreaID,
        a.name AS AreaName,
        crmr.dateocc AS CrimeDate,
		LAG(crmr.dateocc) OVER (PARTITION BY AreaID ORDER BY crmr.dateocc ) AS PrevCrimeDate
    FROM
        crimesrecords crmr
    INNER JOIN crimeseverities cs ON cs.crimerecordid = crmr.id
    INNER JOIN crimes c ON c.id = cs.crimeid
    INNER JOIN subareas sa ON sa.id = crmr.subareaid
    INNER JOIN areas a ON a.id = sa.areaid
    WHERE
        c.code = @code
),
gaps AS (
    SELECT
        AreaID,
        AreaName,
		PrevCrimeDate,
        CrimeDate AS CurrentCrimeDate,
        EXTRACT(DAY FROM (CrimeDate - PrevCrimeDate))  AS GapDays
    FROM
        crime_date_ranges
    WHERE
        PrevCrimeDate IS NOT NULL
 ),
max_gap_per_area AS (
    SELECT
        AreaID,
        AreaName,
        MAX(GapDays) AS MaxGapDays
    FROM
        gaps
    GROUP BY
        AreaID,
        AreaName
 ),
max_gap_details AS (
    SELECT
        g.AreaID,
        g.AreaName,
        g.PrevCrimeDate AS StartDate,
        g.CurrentCrimeDate AS EndDate,
        g.GapDays
    FROM
        gaps g
    INNER JOIN max_gap_per_area m ON
        g.AreaID = m.AreaID AND g.GapDays = m.MaxGapDays
 )
SELECT
    AreaName,
    StartDate,
    EndDate,
    GapDays AS MaxGapDays
FROM
    max_gap_details
ORDER BY
    MaxGapDays DESC
LIMIT 1;
```

### Dpt Dist No
```sql
WITH crime_date_ranges AS (
    SELECT
        sa.id AS SubAreaID,
        sa.rpddistno AS RptDistNo,
        crmr.dateocc AS CrimeDate,
		LAG(crmr.dateocc) OVER (PARTITION BY SubAreaID ORDER BY crmr.dateocc ) AS PrevCrimeDate
    FROM
        crimesrecords crmr
    INNER JOIN crimeseverities cs ON cs.crimerecordid = crmr.id
    INNER JOIN crimes c ON c.id = cs.crimeid
    INNER JOIN subareas sa ON sa.id = crmr.subareaid
    WHERE
        c.code = @code
),
gaps AS (
    SELECT
        RptDistNo,
        PrevCrimeDate,
        CrimeDate AS CurrentCrimeDate,
        EXTRACT(EPOCH FROM (CrimeDate - PrevCrimeDate)) / 86400 AS GapDays
    FROM
        crime_date_ranges
    WHERE
        PrevCrimeDate IS NOT NULL
),
max_gap_per_rptdistno AS (
    SELECT
        RptDistNo,
        MAX(GapDays) AS MaxGapDays
    FROM
        gaps
    GROUP BY
        RptDistNo
),
max_gap_details AS (
    SELECT
        g.RptDistNo,
        g.PrevCrimeDate AS StartDate,
        g.CurrentCrimeDate AS EndDate,
        g.GapDays
    FROM
        gaps g
    INNER JOIN max_gap_per_rptdistno m ON
        g.RptDistNo = m.RptDistNo AND g.GapDays = m.MaxGapDays
)
SELECT
    RptDistNo,
    StartDate,
    EndDate,
    GapDays AS MaxGapDays
FROM
    max_gap_details
ORDER BY
    MaxGapDays DESC
LIMIT 1;
```
### **Query 11**: For 2 crimes of your choice, find all areas that have received more than 1 report on each of these 2 crimes on the same day. The 2 crimes could be for example: “CHILD ANNOYING (17YRS & UNDER)” or “THEFT OF IDENTITY”. Do not restrict yourself to just these 2 specific types of crimes of course!
```sql
WITH crime_counts AS (
    SELECT
        a.name AS AreaName,
        cr.dateocc AS CrimeDate,
        c1.code AS Crime1Code,
        c2.code AS Crime2Code,
        COUNT(*) AS CrimesCount
    FROM
        crimesrecords cr
    INNER JOIN crimeseverities cs1 ON cs1.crimerecordid = cr.id
    INNER JOIN crimes c1 ON c1.id = cs1.crimeid
    INNER JOIN crimeseverities cs2 ON cs2.crimerecordid = cr.id AND cs2.id <> cs1.id
    INNER JOIN crimes c2 ON c2.id = cs2.crimeid
    INNER JOIN subareas sa ON sa.id = cr.subareaid
    INNER JOIN areas a ON a.id = sa.areaid
    WHERE
	    c1.code = @code1 AND c2.code = @code2
    GROUP BY
        sa.areaid, a.name, cr.dateocc, c1.code, c2.code
    )
SELECT
    AreaName,
    CrimeDate,
    Crime1Code,
    Crime2Code,
    CrimesCount
FROM
    crime_counts
WHERE
    CrimesCount > 1
ORDER BY
    AreaName, CrimeDate;
```
### **Query 12**: Find the number of division of records for crimes reported on the same day in different areas using the same weapon for a specific time range.
```sql
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
```

### **Query 13**: Find the crimes that occurred for a given number of times N on the same day, in the same area, using the same weapon, for a specific time range. Return the list of division of records numbers, the area name, the crime code description and the weapon description.
```sql
WITH CrimeOccurrences AS (
    SELECT
        crmr.dateocc AS CrimeDate,
        a."name" AS AreaName,
		w.code AS WeaponCode,
        w."desc" AS WeaponDesc,
		c.code AS CrimeCode,
        c."desc" AS CrimeDesc,
        crmr.drno AS CrimeDrNo
    FROM
        crimesrecords crmr
    INNER JOIN crimeseverities crms ON crms.crimerecordid = crmr.id
    INNER JOIN crimes c ON c.id = crms.crimeid
    INNER JOIN weapons w ON w.id = crmr.weaponid
    INNER JOIN subareas sa ON sa.id = crmr.subareaid
    INNER JOIN areas a ON a.id = sa.areaid
    WHERE
        crmr.dateocc + crmr.timeocc BETWEEN @startDate AND @endDate
)
SELECT
	CrimeDate,
	AreaName,
	WeaponCode,
	WeaponDesc,
	CrimeCode,
	CrimeDesc,
	STRING_AGG(CrimeDrNo, ', ') AS ListOfDrNo
FROM
	CrimeOccurrences
GROUP BY
	CrimeDate,
	AreaName,
	WeaponCode,
	WeaponDesc,
	CrimeCode,
	CrimeDesc
HAVING COUNT(*) = @N
```
