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
4. [Screen Shots](#screen-shots)

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

### Query 1: Find the total number of reports per “Crm Cd” that occurred within a specified time range and sort them in descending order
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

### Query 2: Find the total number of reports per day for a specific “Crm Cd” and time range.
```sql

```

### Query 3: Find the most common crime committed regardless of code 1, 2, 3, and 4, per area for a specific day.
```sql

```
### Query 4: Find the average number of crimes occurred per hour (24 hours) for a specific date range.
```sql

```
### Query 5: Find the mostcommon“Crm Cd”inaspecifiedboundingbox(asdesignated by GPS-coordinates) for a specific day.
```sql

```
### Query 6:Find the top-5 Area names with regards to total number of crimes reported per day for a specific date range. The same for Rpt Dist No.
```sql

```
### Query 7: Find the pair of crimes that has co-occurred in the area with the most reported incidents for a specific date range.
```sql

```
### Query 8: Find the second most common crime that has co-occurred with a particular crime for a specifi date range.
```sql

```
### Query 9: Find the most common type of weapon used against victims depending on their group of age. The age groups are formed by bucketing ages every 5 years, e.g., 0 ≤ x < 5, 5 ≤ x < 10, etc..
```sql

```
### Query 10: Find the area with the longest time range without an occurrence of a specific crime. Include the time range in the results. The same for Rpt Dist No.
```sql

```
### Query 11: For 2 crimes of your choice, find all areas that have received more than 1 report on each of these 2 crimes on the same day. The 2 crimes could be for example: “CHILD ANNOYING (17YRS & UNDER)” or “THEFT OF IDENTITY”. Do not restrict yourself to just these 2 specific types of crimes of course!
```sql

```
### Query 12: Find the number of division of records for crimes reported on the same day in different areas using the same weapon for a specific time range.
```sql

```

### Query 13: Find the crimes that occurred for a given number of times N on the same day, in the same area, using the same weapon, for a specific time range. Return the list of division of records numbers, the area name, the crime code description and the weapon description.
```sql

```
