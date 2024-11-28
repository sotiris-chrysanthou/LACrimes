using LACrimes.EF.Repository;
using LACrimes.Model;
using LACrimes.Web.Blazor.Shared;
using System.Xml.Linq;

namespace LACrimes.Web.Blazor.Server.Helpers {
    internal class CrmRctrlHelper {

        internal static async Task ManageCrimeSeverities(CrimeRecordDto crimeRecordDto, CrimeRecord crimeRecord) {
            CrimeSeverityRepo crimeSeverityRepo = new CrimeSeverityRepo();
            IList<CrimeSeverity> dbCrimeSeverities = await new CrimeSeverityRepo().GetAll(cs => cs.CrimeRecordID == crimeRecord.ID);
            foreach(var crimeSeverityDto in crimeRecordDto.CrimeSeverities) {
                // Find CrimeSeverities that are in the CrimeRecordDto
                CrimeSeverity? dbCrimeSeverity = dbCrimeSeverities.FirstOrDefault(cs => cs.CrimeID == crimeSeverityDto.CrimeID && cs.Severity == crimeSeverityDto.Severity);
                // Add CrimeSeverities that are not in the CrimeRecordDto
                if(dbCrimeSeverity == null) {
                    CrimeSeverity crimeSeverity = await CreateCrimeSeverity(crimeRecord.ID, crimeSeverityDto);
                    await crimeSeverityRepo.Add(crimeSeverity);
                    continue;
                }
                // Remove CrimeSeverities that are in the CrimeRecordDto
                dbCrimeSeverities.Remove(dbCrimeSeverity);
            }
            // Delete CrimeSeverities that are not in the CrimeRecordDto
            foreach(var dbCrimeSeverity in dbCrimeSeverities) {
                await crimeSeverityRepo.Delete(dbCrimeSeverity.ID);
            }
        }

        internal static async Task PostNewCrimeSeverities(CrimeRecordDto crimeRecordDto) {
            foreach(var crimeSeverityDto in crimeRecordDto.CrimeSeverities) {
                CrimeSeverityRepo crimeSeverityRepo = new CrimeSeverityRepo();
                CrimeSeverity crimeSeverity = await CreateCrimeSeverity(crimeRecordDto.ID, crimeSeverityDto);
                await crimeSeverityRepo.Add(crimeSeverity);
            }
        }

        internal static async Task<CrimeSeverity> CreateCrimeSeverity(Guid? crimeRecordID, CrimeSeverityDto crimeSeverityDto) {
            if(crimeRecordID is null) {
                throw new ArgumentNullException(nameof(crimeRecordID));
            }

            CrimeRepo crimeRepo = new CrimeRepo();
            IList<Crime> crimeList = await crimeRepo.GetAll(c => c.Code == crimeSeverityDto.Code && c.Desc == crimeSeverityDto.Desc);
            if(crimeList.Count > 0) {
                return new CrimeSeverity {
                    ID = Guid.NewGuid(),
                    CrimeID = crimeList[0].ID,
                    CrimeRecordID = (Guid)crimeRecordID,
                    Severity = crimeSeverityDto.Severity
                };
            }
            return new CrimeSeverity {
                ID = Guid.NewGuid(),
                Crime = new Crime(crimeSeverityDto.Code, crimeSeverityDto.Desc),
                CrimeRecordID = (Guid)crimeRecordID,
                Severity = crimeSeverityDto.Severity
            };
        }

        internal static async Task<(CrimeRecord, laLists?)> CreateCrimeRecord(CrimeRecordDto crimeRecordDto, Guid? id = null, laLists? lists = null) {

            (SubArea? subArea, bool isNewSubArea, lists) = await FetchOrCreateSubArea(crimeRecordDto, lists);
            if(subArea is null) {
                throw new Exception("Fill all fields in Area section or choose an existing one");
            }
            (Victim? victim, bool isNewVictim, lists) = await FetchOrCreateVictim(crimeRecordDto, lists);
            if(victim is null) {
                throw new Exception("Fill at least age in Victim section or choose an existing one");
            }
            (Premis? premis, bool isNewPremis, lists) = await FetchOrCreatePremis(crimeRecordDto, lists);
            (Weapon? weapon, bool isNewWeapon, lists) = await FetchOrCreateWeapon(crimeRecordDto, lists);
            (Status? status, bool isNewStatus, lists) = await FetchOrCreateStatus(crimeRecordDto, lists);
            if(status is null) {
                throw new Exception("Fill status code and description in Status section or choose an existing one");
            }
            (Street? street, bool isNewStreet, lists) = await FetchOrCreateStreet(crimeRecordDto, lists);
            if(street is null) {
                throw new Exception("Fill at street name in Street section or choose an existing one");
            }
            (Street? crossStreet, bool isNewCrossStreet, lists) = await FetchOrCreateCrossStreet(crimeRecordDto, lists);
            (Coordinates? coordinates, bool isNewCoordinates, lists) = await FetchOrCreateCoordinates(crimeRecordDto, lists);

            var crimeRecord = new CrimeRecord {
                ID = crimeRecordDto.ID ?? Guid.NewGuid(),
                DrNo = crimeRecordDto.DrNo,
                DateOcc = crimeRecordDto.DateOcc.ToUniversalTime(),
                DateRptd = crimeRecordDto.DateRptd.ToUniversalTime(),
                TimeOcc = crimeRecordDto.TimeOcc,
            };
            LinkSubAreaToCrimeRecord(subArea, isNewSubArea, crimeRecord);
            LinkVictimToCrimeRecord(victim, isNewVictim, crimeRecord);
            LinkPremisToCrimeRecord(premis, isNewPremis, crimeRecord);
            LinkWeaponToCrimeRecord(weapon, isNewWeapon, crimeRecord);
            LinkStatusToCrimeRecord(status, isNewStatus, crimeRecord);
            LinkStreetToCrimeRecord(street, isNewStreet, crimeRecord);
            LinkCrossStreetToCrimeRecord(crossStreet, isNewCrossStreet, crimeRecord);
            LinkCoordinatesToCrimeRecord(coordinates, isNewCoordinates, crimeRecord);

            return (crimeRecord,lists);
        }


        internal static void LinkCoordinatesToCrimeRecord(Coordinates? coordinates, bool isNewCoordinates, CrimeRecord crimeRecord) {
            if(isNewCoordinates && coordinates is not null) {
                crimeRecord.Coordinates = coordinates;
            } else if(coordinates is not null) {
                crimeRecord.CoordinatesID = coordinates.ID;
            }
        }

        internal static void LinkCrossStreetToCrimeRecord(Street? crossStreet, bool isNewCrossStreet, CrimeRecord crimeRecord) {
            if(isNewCrossStreet && crossStreet is not null) {
                crimeRecord.CrossStreet = crossStreet;
            } else if(crossStreet is not null) {
                crimeRecord.CrossStreetID = crossStreet.ID;
            }
        }

        internal static void LinkStreetToCrimeRecord(Street street, bool isNewStreet, CrimeRecord crimeRecord) {
            if(isNewStreet && street is not null) {
                crimeRecord.Street = street;
            } else if(street is not null) {
                crimeRecord.StreetID = street.ID;
            }
        }

        internal static void LinkStatusToCrimeRecord(Status status, bool isNewStatus, CrimeRecord crimeRecord) {
            if(isNewStatus && status is not null) {
                crimeRecord.Status = status;
            } else if(status is not null) {
                crimeRecord.StatusID = status.ID;
            }
        }

        internal static void LinkWeaponToCrimeRecord(Weapon? weapon, bool isNewWeapon, CrimeRecord crimeRecord) {
            if(isNewWeapon && weapon is not null) {
                crimeRecord.Weapon = weapon;
            } else if(weapon is not null) {
                crimeRecord.WeaponID = weapon.ID;
            }
        }

        internal static void LinkPremisToCrimeRecord(Premis? premis, bool isNewPremis, CrimeRecord crimeRecord) {
            if(isNewPremis && premis is not null) {
                crimeRecord.Premis = premis;
            } else if(premis is not null) {
                crimeRecord.PremisID = premis.ID;
            }
        }

        internal static void LinkVictimToCrimeRecord(Victim victim, bool isNewVictim, CrimeRecord crimeRecord) {
            if(isNewVictim && victim is not null) {
                crimeRecord.Victim = victim;
            } else if(victim is not null) {
                crimeRecord.VictimID = victim.ID;
            }
        }

        internal static void LinkSubAreaToCrimeRecord(SubArea? subArea, bool isNewSubArea, CrimeRecord crimeRecord) {
            if(isNewSubArea && subArea is not null) {
                crimeRecord.SubArea = subArea;
            } else if(subArea is not null) {
                crimeRecord.SubAreaID = subArea.ID;
            }
        }

        internal static async Task<(Coordinates? coordinates, bool isNewCoordinates, laLists?)> FetchOrCreateCoordinates(CrimeRecordDto crimeRecordDto, laLists? lists = null) {
            CoordinatesRepo coordinatesRepo = new CoordinatesRepo();
            Coordinates? coordinates = null;
            bool isNewCoordinates = false;

            // If the CoordinatesID is empty, then we need to create a new Coordinates
            if((crimeRecordDto.CoordinatesID == null || crimeRecordDto.CoordinatesID == Guid.Empty)) {
                if(lists is not null && lists.Coordinates.Any(c => c.Lat == crimeRecordDto.Lat && c.Lon == crimeRecordDto.Lon)) {
                    coordinates = lists.Coordinates.First(c => c.Lat == crimeRecordDto.Lat && c.Lon == crimeRecordDto.Lon);
                    return (coordinates, isNewCoordinates, lists);
                }
                IList<Coordinates> coordinatesList = (await coordinatesRepo.GetAll(c => c.Lat == crimeRecordDto.Lat && c.Lon == crimeRecordDto.Lon));
                if(coordinatesList.Any()) {
                    coordinates = coordinatesList[0];
                    if(lists is not null) {
                        lists.Coordinates.Add(coordinates);
                    }
                    return (coordinates, isNewCoordinates, lists);
                }
                coordinates = new Coordinates {
                    ID = Guid.NewGuid(),
                    Lat = crimeRecordDto.Lat,
                    Lon = crimeRecordDto.Lon
                };
                isNewCoordinates = true;
                if(lists is not null) {
                    lists.Coordinates.Add(coordinates);
                }
            }
            // If the CoordinatesID is not empty, then we need to fetch the Coordinates
            else if(crimeRecordDto.CoordinatesID != null && crimeRecordDto.CoordinatesID != Guid.Empty) {
                Guid coordinatesID = (Guid)crimeRecordDto.CoordinatesID;
                coordinates = await coordinatesRepo.GetById(coordinatesID);
            }
            return (coordinates, isNewCoordinates, lists);
        }

        internal static async Task<(Street? street, bool isNewStreet, laLists?)> FetchOrCreateStreet(CrimeRecordDto crimeRecordDto, laLists? lists = null) {
            StreetRepo streetRepo = new StreetRepo();
            Street? street = null;
            bool isNewStreet = false;

            // If the StreetID is empty, then we need to create a new Street
            if((crimeRecordDto.StreetID == null || crimeRecordDto.StreetID == Guid.Empty) &&
                !string.IsNullOrEmpty(crimeRecordDto.StreetName)) {
                if(lists is not null && lists.Streets.Any(s => s.Name == crimeRecordDto.StreetName)) {
                    street = lists.Streets.First(s => s.Name == crimeRecordDto.StreetName);
                    return (street, isNewStreet, lists);
                }
                IList<Street>? streetList = await streetRepo.GetAll(streetRepo => streetRepo.Name == crimeRecordDto.StreetName);
                if(streetList.Any()) {
                    street = streetList[0];
                    if(lists is not null) {
                        lists.Streets.Add(street);
                    }
                    return (street, isNewStreet, lists);
                }
                street = new Street {
                    ID = Guid.NewGuid(),
                    Name = crimeRecordDto.StreetName
                };
                if(lists is not null) {
                    lists.Streets.Add(street);
                }
                isNewStreet = true;
            }
            // If the StreetID is not empty, then we need to fetch the Street
            else if(crimeRecordDto.StreetID != null && crimeRecordDto.StreetID != Guid.Empty) {
                Guid streetID = (Guid)crimeRecordDto.StreetID;
                street = await streetRepo.GetById(streetID);
            }

            return (street, isNewStreet, lists);
        }

        internal static async Task<(Street? street, bool isNewStreet, laLists?)> FetchOrCreateCrossStreet(CrimeRecordDto crimeRecordDto, laLists? lists = null) {
            StreetRepo streetRepo = new StreetRepo();
            Street? street = null;
            bool isNewStreet = false;

            // If the StreetID is empty, then we need to create a new Street
            if((crimeRecordDto.CrossStreetID == null || crimeRecordDto.CrossStreetID == Guid.Empty) &&
                !string.IsNullOrEmpty(crimeRecordDto.CrossStreetName)) {
                if(lists is not null && lists.Streets.Any(s => s.Name == crimeRecordDto.CrossStreetName)) {
                    street = lists.Streets.First(s => s.Name == crimeRecordDto.CrossStreetName);
                    return (street, isNewStreet, lists);
                }
                IList<Street>? crossStreetList = await streetRepo.GetAll(streetRepo => streetRepo.Name == crimeRecordDto.CrossStreetName);
                if(crossStreetList.Any()) {
                    street = crossStreetList[0];
                    if(lists is not null) {
                        lists.Streets.Add(street);
                    }
                    return (street, isNewStreet, lists);
                }
                street = new Street {
                    ID = Guid.NewGuid(),
                    Name = crimeRecordDto.CrossStreetName
                };
                if(lists is not null) {
                    lists.Streets.Add(street);
                }
                isNewStreet = true;
            }
            // If the StreetID is not empty, then we need to fetch the Street
            else if(crimeRecordDto.CrossStreetID != null && crimeRecordDto.CrossStreetID != Guid.Empty) {
                Guid CrossStreetID = (Guid)crimeRecordDto.CrossStreetID;
                street = await streetRepo.GetById(CrossStreetID);
            }

            return (street, isNewStreet, lists);
        }

        internal static async Task<(Status? status, bool isNewStatus, laLists?)> FetchOrCreateStatus(CrimeRecordDto crimeRecordDto, laLists? lists = null) {
            StatusRepo statusRepo = new StatusRepo();
            Status? status = null;
            bool isNewStatus = false;

            // If the StatusID is empty, then we need to create a new Status
            if((crimeRecordDto.StatusID == null || crimeRecordDto.StatusID == Guid.Empty) &&
                !string.IsNullOrEmpty(crimeRecordDto.StatusCode) && !string.IsNullOrEmpty(crimeRecordDto.StatusDesc)) {
                if(lists is not null && lists.Statuses.Any(s => s.Code == crimeRecordDto.StatusCode)) {
                    status = lists.Statuses.First(s => s.Code == crimeRecordDto.StatusCode);
                    if(status.Desc != crimeRecordDto.StatusDesc) {
                        throw new Exception($"Status code {status.Code} already exists with a different description {status.Desc}. DrNo: {crimeRecordDto.DrNo}");
                    }
                    return (status, isNewStatus, lists);
                }
                IList<Status>? statuses = await statusRepo.GetAll(s => s.Code == crimeRecordDto.StatusCode);
                if(statuses.Any()) {
                    status = statuses[0];
                    if(status.Desc != crimeRecordDto.StatusDesc) {
                        throw new Exception($"Status code {status.Code} already exists with a different description {status.Desc}. DrNo: {crimeRecordDto.DrNo}");
                    }
                    if(lists is not null) {
                        lists.Statuses.Add(status);
                    }
                    return (status, isNewStatus, lists);
                }
                status = new Status {
                    ID = Guid.NewGuid(),
                    Code = crimeRecordDto.StatusCode,
                    Desc = crimeRecordDto.StatusDesc
                };
                if(lists is not null) {
                    lists.Statuses.Add(status);
                }
                isNewStatus = true;
            }
            // If the StatusID is not empty, then we need to fetch the Status
            else if(crimeRecordDto.StatusID != null && crimeRecordDto.StatusID != Guid.Empty) {
                Guid statusID = (Guid)crimeRecordDto.StatusID;
                status = await statusRepo.GetById(statusID);
            }

            return (status, isNewStatus, lists);
        }

        internal static async Task<(Weapon? weapon, bool isNewWeapon, laLists?)> FetchOrCreateWeapon(CrimeRecordDto crimeRecordDto, laLists? lists = null) {
            WeaponRepo weaponRepo = new WeaponRepo();
            Weapon? weapon = null;
            bool isNewWeapon = false;

            // If the WeaponID is empty, then we need to create a new Weapon
            if((crimeRecordDto.WeaponID == null || crimeRecordDto.WeaponID == Guid.Empty) &&
               (crimeRecordDto.WeaponCode != null && crimeRecordDto.WeaponCode != 0 && crimeRecordDto.WeaponDesc != null)) {
                if(lists is not null && lists.Weapons.Any(w => w.Code == crimeRecordDto.WeaponCode)) {
                    weapon = lists.Weapons.First(w => w.Code == crimeRecordDto.WeaponCode);
                    if(weapon.Desc != crimeRecordDto.WeaponDesc) {
                        throw new Exception($"Weapon code {weapon.Code} already exists with a different description {weapon.Desc}. DrNo: {crimeRecordDto.DrNo}");
                    }
                    return (weapon, isNewWeapon, lists);
                }
                IList<Weapon>? weapons = await weaponRepo.GetAll(w => w.Code == crimeRecordDto.WeaponCode);
                if(weapons.Any()) {
                    weapon = weapons[0];
                    if(weapon.Desc != crimeRecordDto.WeaponDesc) {
                        throw new Exception($"Weapon code {weapon.Code} already exists with a different description {weapon.Desc}. DrNo: {crimeRecordDto.DrNo}");
                    }
                    if(lists is not null) {
                        lists.Weapons.Add(weapon);
                    }
                    return (weapon, isNewWeapon, lists);
                }
                weapon = new Weapon {
                    ID = Guid.NewGuid(),
                    Code = (int)crimeRecordDto.WeaponCode,
                    Desc = crimeRecordDto.WeaponDesc
                };
                if(lists is not null) {
                    lists.Weapons.Add(weapon);
                }
                isNewWeapon = true;
            }
            // If the WeaponID is not empty, then we need to fetch the Weapon
            else if(crimeRecordDto.WeaponID != null && crimeRecordDto.WeaponID != Guid.Empty) {
                Guid weaponID = (Guid)crimeRecordDto.WeaponID;
                weapon = await weaponRepo.GetById(weaponID);
            }

            return (weapon, isNewWeapon, lists);
        }

        internal static async Task<(Premis? premis, bool isNewPremis, laLists?)> FetchOrCreatePremis(CrimeRecordDto crimeRecordDto, laLists? lists = null) {
            PremisRepo premisRepo = new PremisRepo();
            Premis? premis = null;
            bool isNewPremis = false;

            // If the PremisID is empty, then we need to create a new Premis
            if((crimeRecordDto.PremisID == null || crimeRecordDto.PremisID == Guid.Empty)
                                                && crimeRecordDto.PremisCode != null && crimeRecordDto.PremisCode != 0
                                                && crimeRecordDto.PremisDesc != null && crimeRecordDto.PremisDesc != String.Empty) {
                if(lists is not null && lists.Premises.Any(p => p.Code == crimeRecordDto.PremisCode)) {
                    premis = lists.Premises.First(p => p.Code == crimeRecordDto.PremisCode);
                    if(premis.Desc != crimeRecordDto.PremisDesc) {
                        throw new Exception($"Premis code {premis.Code} already exists with a different description {premis.Desc}. DrNo: {crimeRecordDto.DrNo}");
                    }
                    return (premis, isNewPremis, lists);
                }
                IList<Premis>? premisList = await premisRepo.GetAll(p => p.Code == crimeRecordDto.PremisCode);
                if(premisList.Any()) {
                    premis = premisList[0];
                    if(premis.Desc != crimeRecordDto.PremisDesc) {
                        throw new Exception($"Premis code {premis.Code} already exists with a different description {premis.Desc}. DrNo: {crimeRecordDto.DrNo}");
                    }
                    if(lists is not null) {
                        lists.Premises.Add(premis);
                    }
                    return (premis, isNewPremis, lists);
                }
                premis = new Premis {
                    ID = Guid.NewGuid(),
                    Code = (int)crimeRecordDto.PremisCode,
                    Desc = crimeRecordDto.PremisDesc is not null && crimeRecordDto.PremisDesc.Length > 0 ? crimeRecordDto.PremisDesc : null
                };
                if(lists is not null) {
                    lists.Premises.Add(premis);
                }
                isNewPremis = true;
            }
            // If the PremisID is not empty, then we need to fetch the Premis
            else if(crimeRecordDto.PremisID != null && crimeRecordDto.PremisID != Guid.Empty) {
                Guid premisID = (Guid)crimeRecordDto.PremisID;
                premis = await premisRepo.GetById(premisID);
            }

            return (premis, isNewPremis, lists);
        }

        internal static async Task<(Victim? victim, bool isNewVictim, laLists?)> FetchOrCreateVictim(CrimeRecordDto crimeRecordDto, laLists? lists = null) {
            VictimRepo victimRepo = new VictimRepo();
            Victim? victim = null;
            bool isNewVictim = false;

            // If the VictimID is empty, then we need to create a new Victim
            if(crimeRecordDto.VictimID == null || crimeRecordDto.VictimID == Guid.Empty) {
                if(lists is not null && lists.Victims.Any(v => v.Age == crimeRecordDto.VictAge && v.Sex == crimeRecordDto.VictSex && v.Descent == crimeRecordDto.VictimDescent)) {
                    victim = lists.Victims.First(v => v.Age == crimeRecordDto.VictAge && v.Sex == crimeRecordDto.VictSex && v.Descent == crimeRecordDto.VictimDescent);
                    return (victim, isNewVictim, lists);
                }
                string? sex = crimeRecordDto.VictSex is not null && crimeRecordDto.VictSex.Length == 1 ? crimeRecordDto.VictSex : null;
                string? descent = crimeRecordDto.VictimDescent is not null && crimeRecordDto.VictimDescent.Length == 1 ? crimeRecordDto.VictimDescent : null;
                IList<Victim>? victims = await victimRepo.GetAll(v => v.Age == crimeRecordDto.VictAge && v.Sex == sex && v.Descent == descent);
                if(victims.Any()) {
                    victim = victims[0];
                    if(lists is not null) {
                        lists.Victims.Add(victim);
                    }
                    return (victim, isNewVictim, lists);
                }
                victim = new Victim {
                    ID = Guid.NewGuid(),
                    Age = crimeRecordDto.VictAge,
                    Sex = crimeRecordDto.VictSex is not null && crimeRecordDto.VictSex.Length == 1 ? crimeRecordDto.VictSex : null,
                    Descent = crimeRecordDto.VictimDescent is not null && crimeRecordDto.VictimDescent.Length == 1 ? crimeRecordDto.VictimDescent : null
                };
                if(lists is not null) {
                    lists.Victims.Add(victim);
                }
                isNewVictim = true;
            }
            // If the VictimID is not empty, then we need to fetch the Victim
            else if(crimeRecordDto.VictimID != null && crimeRecordDto.VictimID != Guid.Empty) {
                Guid victimID = (Guid)crimeRecordDto.VictimID;
                victim = await victimRepo.GetById(victimID);
            }

            return (victim, isNewVictim, lists);
        }

        internal static async Task<(SubArea?, bool, laLists?)> FetchOrCreateSubArea(CrimeRecordDto crimeRecordDto, laLists? lists = null) {
            SubAreaRepo subAreaRepo = new SubAreaRepo();
            SubArea? subArea = null;
            Area? area;
            bool isNewSubArea = false;
            // If the SubAreaID is empty, then we need to create a new SubArea
            if((crimeRecordDto.SubAreaID == null || crimeRecordDto.SubAreaID == Guid.Empty)
                                                && !String.IsNullOrEmpty(crimeRecordDto.AreaCode)
                                                && !string.IsNullOrEmpty(crimeRecordDto.AreaName)
                                                && !string.IsNullOrEmpty(crimeRecordDto.RpdDistNo.ToString())) {
                if(lists is not null && lists.SubAreas.Any(s => s.RpdDistNo == crimeRecordDto.RpdDistNo)) {
                    subArea = lists.SubAreas.First(s => s.RpdDistNo == crimeRecordDto.RpdDistNo);
                    return (subArea, isNewSubArea, lists);
                }
                String areaCode = crimeRecordDto.AreaCode;
                String areaName = crimeRecordDto.AreaName;
                String drNo = crimeRecordDto.RpdDistNo;
                (area, bool isNewArea, lists) = await FetchOrCreateArea(areaName, areaCode, drNo, lists);
                IList<SubArea> areas = await subAreaRepo.GetAll(a => a.RpdDistNo == crimeRecordDto.RpdDistNo);
                if(areas.Any()) {
                    subArea = areas[0];
                    if(lists is not null) {
                        lists.SubAreas.Add(subArea);
                    }
                    return (subArea, isNewSubArea, lists);
                }
                subArea = new SubArea {
                    ID = Guid.NewGuid(),
                    RpdDistNo = crimeRecordDto.RpdDistNo
                };
                if(isNewArea)
                    subArea.Area = area;
                else
                    subArea.AreaID = area.ID;
                if(lists is not null) {
                    lists.SubAreas.Add(subArea);
                }
                isNewSubArea = true;
            }
            // If the SubAreaID is not empty, then we need to fetch the SubArea
            else if(crimeRecordDto.SubAreaID != null && crimeRecordDto.SubAreaID != Guid.Empty) {
                Guid subAreaID = (Guid)crimeRecordDto.SubAreaID;
                subArea = await subAreaRepo.GetById(subAreaID);
            }

            return (subArea, isNewSubArea, lists);
        }

        internal static async Task<(Area, bool, laLists?)> FetchOrCreateArea(String areaName, String areaCode, string drNo, laLists? lists = null) {
            AreaRepo areaRepo = new AreaRepo();
            bool isNew = true;
            Area area;
            if(lists is not null && lists.Areas.Any(a => a.Code == areaCode)) {
                area = lists.Areas.First(a => a.Code == areaCode);
                if(area.Name != areaName) {
                    throw new Exception($"Area code {area.Code} already exists with a different name {area.Name}. DrNo: {drNo}");
                }
                isNew = false;
                return (area, isNew, lists);
            }
            IList<Area> areas = await areaRepo.GetAll(a => a.Code == areaCode);
            if(areas.Any()) {
                isNew = false;
                area = areas[0];
                if(area.Name != areaName) {
                    throw new Exception($"Area code {area.Code} already exists with a different name {area.Name}. DrNo: {drNo}");
                }
                if(lists is not null) {
                    lists.Areas.Add(area);
                }
                return (area, isNew, lists);
            }
            area = new Area {
                ID = Guid.NewGuid(),
                Name = areaName,
                Code = areaCode
            };
            if(lists is not null) {
                lists.Areas.Add(area);
            }
            return (area, isNew, lists);
        }
    }
}
