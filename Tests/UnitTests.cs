//using MahCoadz.Kryptow;
//using MahCoadz.Data;
//using MahCoadz.Data.FunnySongNS;
//using MahCoadz.Util;
//using MahCoadz.Uzer;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using MahCoadz.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Tests
//{
//    [TestClass]
//    public class UnitTests
//    {
//        [TestMethod]
//        public void TestHawsh1()
//        {
//            var rezult = genericPostHashes.Hawsh;
//            Assert.AreEqual(rezult.Count, 3);
//            Assert.AreEqual(rezult[1 - 1].Item1, "GenericEntities.SILS1_SillySongs");
//            // this index will change as new entities are added ...test will automatically fail on addition of new entities
//            Assert.AreEqual(rezult[3 - 1].Item1, "GenericEntityPostType.delete");            
//        }

//        [TestMethod]
//        public void TestUzerGenericEntities1Goodpath()
//        {
//            //var rezult = AppConfig.Setting.AppGenericEntities;
//            var SpecialUzerConfigString = "Updater≈upper≈Human≈FunnyMusic;x7oD9xvCmIIi79hF1hYfdwhEaR4;updateorcreate,delete|MUZIK_SCORE;OZkezfAf+WayTgxgyBFHLfAncho;updateorcreate,delete■Viewer≈view≈Human■fjke903kjldf903≈D:\\Test_23f3|true|true|.txt:.pdf:.doc:.docx:.xlsx:.epub:||assets";
//            var rezult = UzerTools.GimmeStaticUzers(false, SpecialUzerConfigString);

//            Assert.AreEqual(rezult.Count, 3);
//            Assert.AreEqual(rezult[2].postkeys, "FunnyMusic;x7oD9xvCmIIi79hF1hYfdwhEaR4;updateorcreate,delete|MUZIK_SCORE;OZkezfAf+WayTgxgyBFHLfAncho;updateorcreate,delete");
//            Assert.AreEqual(string.IsNullOrEmpty(rezult[10].postkeys), true); // FunnyMusic,MUZIK_SCORE");

//            var rezult2 = JsonAPITool.CompilePostEntities(rezult[9].postkeys);
//            Assert.AreEqual(rezult2.Count, 2);

//            rezult2 = JsonAPITool.CompilePostEntities(rezult[10].postkeys);
//            Assert.AreEqual(rezult2.Count, 0);

//        }
//        //todo: a test to show all the dataentity hashes - to make it easier to build postkey config strings...

//        [TestMethod]
//        public void TestAppConfigGenericEntities1Goodpath()
//        {
//            //var rezult = AppConfig.Setting.AppGenericEntities;
//            var rezult = JsonAPITool.CompilePostEntities("FunnyMusic;x7oD9xvCmIIi79hF1hYfdwhEaR4;updateorcreate,delete|");
//            Assert.AreEqual(rezult.Count, 1);
//            Assert.AreEqual(rezult[0].PostTypes.Count, 2);
//        }

//        [TestMethod]
//        public void TestAppConfigGenericEntities2BadDataPath()
//        {
//            //var rezult = AppConfig.Setting.AppGenericEntities;
//            try
//            {
//                var rezult = JsonAPITool.CompilePostEntities("FunnyMusic;x7oD9xvCmIIi79hF1hYfdwhEaR4=;create,update,updateorcreate,delete|DummyEntitey;20985783;bogusAction|");
//                Assert.Fail();
//            }
//            catch (Exception ex)
//            {
//                Assert.AreEqual(ex.Message, "Bad Data Entity configuration values..");
//            }
//        }

//        [TestMethod]
//        public void TestPost1GoodpathUpdate()
//        {
//            var BlobExample = "ew0KICAgICJJZCI6IDg5LA0KICAgICJUaXRsZSI6ICJJ4oCZbSBGdWxsIG9mIFN0ZWFrLCBhbmQgQ2Fubm90IERhbmNlIiwNCiAgICAiU3ViVGl0bGUiOiBudWxsLA0KICAgICJDb21wb3NlciI6ICJTaWRuZXkgR2lzaCIsDQogICAgIkNvbXBvc2VyUGlja2VySUQiOiBudWxsLA0KICAgICJMeXJpY2lzdCI6IG51bGwsDQogICAgIkFycmFuZ2VyIjogbnVsbCwNCiAgICAiUHVibGlzaGVyIjogIk1FVFJJRlkgQVJUSUZJQ0VSIElOQy4iLA0KICAgICJDb3B5cmlnaHROdW1iZXIiOiBudWxsLA0KICAgICJVc2VyUGVybWlzc2lvbklmTm90RkdTIjogbnVsbCwNCiAgICAiUHJpY2VQZXJDb3B5IjogbnVsbCwNCiAgICAiUHJpY2VQZXJDb3B5QXNPZiI6IG51bGwsDQogICAgIlN0eWxlIjogIlJhcC9UcmFwL1NrYSIsDQogICAgIkluc3RydW1lbnRhdGlvbiI6IG51bGwsDQogICAgIkxldmVsIjogbnVsbCwNCiAgICAiSXNBY3RpdmUiOiB0cnVlLA0KICAgICJNb2RpZmllZEJ5IjogIkdvb2dsZXBsZXggU3RhcnRoaW5rZXIiLA0KICAgICJNb2RpZmllZEJ5VXNlcklkIjogbnVsbCwNCiAgICAiTW9kaWZpZWRPbiI6ICIyMDA4LTA5LTIwVDA1OjM5OjQ1LjkzIiwNCiAgICAiQ3JlYXRlZEJ5IjogIlRyaW4gVHJhZ3VsYSIsDQogICAgIkNyZWF0ZWRCeUlkIjogbnVsbCwNCiAgICAiQ3JlYXRlZE9uIjogIjIwMDgtMDktMTJUMTQ6NTM6MzUuOTMiLA0KICAgICJJbXBvcnRlZE9uIjogbnVsbCwNCiAgICAiU29tZVJhbmRvbVZhbHVlIjogMA0KICB9";
//            var thisPost = new genericPostInfo()
//            {
//                EntityTag = "FunnyMusic", // AllowedGenericEntities.FunnyMusic.ToString(),
//                PostType = AllowedGenericEntityPostType.updateorcreate.ToString(),
//                PostDataJSON = BlobExample
//            };
//            /* {
//              "EntityTag": "string",
//              "PostType": "string",
//              "DataBase64Blob": "string",
//              "responseString": "string",
//              "responseCode": 0
//            }
//            */
//            //var rezult = JsonAPITool.processGenericPostInfo(thisPost, JsonAPITool.CompilePostEntities("FunnyMusic;x7oD9xvCmIIi79hF1hYfdwhEaR4=;create,update,updateorcreate,delete|"));
//            //Assert.AreEqual(rezult.responseCode, genericPostResponses.UpdatedCompleted);
//            // at this time - this test requires you to go to the db and see if the metaUpdated field is updated to the current time...
//        }

//        [TestMethod]
//        public void TestPost1GoodpathAdd()
//        {
//            // I'm a little teapot title with ID=0 - should make it add it..
//            var BlobExample = "ew0KICAgICJJZCI6IDAsDQogICAgIlRpdGxlIjogIknigJltIOKAmUHigJkgTGl0dGxlIFRlYSdkICdQb3QnIiwNCiAgICAiU3ViVGl0bGUiOiBudWxsLA0KICAgICJDb21wb3NlciI6ICJTaWRuZXkgR2lzaCIsDQogICAgIkNvbXBvc2VyUGlja2VySUQiOiBudWxsLA0KICAgICJMeXJpY2lzdCI6IG51bGwsDQogICAgIkFycmFuZ2VyIjogbnVsbCwNCiAgICAiUHVibGlzaGVyIjogIk1FVFJJRlkgQVJUSUZJQ0VSIElOQy4iLA0KICAgICJDb3B5cmlnaHROdW1iZXIiOiBudWxsLA0KICAgICJVc2VyUGVybWlzc2lvbklmTm90RkdTIjogbnVsbCwNCiAgICAiUHJpY2VQZXJDb3B5IjogbnVsbCwNCiAgICAiUHJpY2VQZXJDb3B5QXNPZiI6IG51bGwsDQogICAgIlN0eWxlIjogIlJhcC9UcmFwL1NrYSIsDQogICAgIkluc3RydW1lbnRhdGlvbiI6IG51bGwsDQogICAgIkxldmVsIjogbnVsbCwNCiAgICAiSXNBY3RpdmUiOiB0cnVlLA0KICAgICJNb2RpZmllZEJ5IjogIkdvb2dsZXBsZXggU3RhcnRoaW5rZXIiLA0KICAgICJNb2RpZmllZEJ5VXNlcklkIjogbnVsbCwNCiAgICAiTW9kaWZpZWRPbiI6ICIyMDA4LTA5LTIwVDA1OjM5OjQ1LjkzIiwNCiAgICAiQ3JlYXRlZEJ5IjogIlRyaW4gVHJhZ3VsYSIsDQogICAgIkNyZWF0ZWRCeUlkIjogbnVsbCwNCiAgICAiQ3JlYXRlZE9uIjogIjIwMDgtMDktMTJUMTQ6NTM6MzUuOTMiLA0KICAgICJJbXBvcnRlZE9uIjogbnVsbCwNCiAgICAiU29tZVJhbmRvbVZhbHVlIjogMA0KICB9";
//            //todo: rewrite to use new EntityTag naming convention..
//            var thisPost = new genericPostInfo()
//            {
//                EntityTag = "FunnyMusic", //AllowedGenericEntities.FunnyMusic.ToString(),
//                PostType = AllowedGenericEntityPostType.updateorcreate.ToString(),
//                PostDataJSON = BlobExample
//            };
//            //var rezult = JsonAPITool.processGenericPostInfo(thisPost, JsonAPITool.CompilePostEntities("FunnyMusic;x7oD9xvCmIIi79hF1hYfdwhEaR4=;create,update,updateorcreate,delete|"));
//            //Assert.AreEqual(rezult.responseCode, genericPostResponses.UpdatedCompleted);
//            // at this time - this test requires you to go to the db and see if the new record exists...
//        }



//        [TestMethod]
//        public void TestDbRetrieve1()
//        {
//            var expectedrows = 84;
//            var rezultSet = new MahDataMgr().RetrieveAll("SILS1", "SillySongs");
//            Assert.AreEqual(expectedrows, rezultSet.Count);
//        }

//        //todo: Structure to Json via App config ? DALHelper?
//        //[TestMethod]
//        //public void TestDalDAO1()
//        //{
//        //    var expectedrows = 3;
//        //    List<DalDAO> rezult = MahCoadz.Data.Tool.DelimitedToDalDaoLst(";:id:id;so:forth;and:soforth;");
//        //    Assert.AreEqual(expectedrows, rezult.Count);
//        //}
//        [TestMethod]
//        public void TestDbRetrieve2()
//        {
//            var expectedrows = 84;
//            var rawrezultSet = new MahDataMgr().RetrieveAll("SILS1", "SillySongs");
//            var rezultSet = FunnySong.FunnySongs(rawrezultSet);
//            Assert.AreEqual(expectedrows, rezultSet.Count);
//        }

//        [TestMethod]
//        public void TestDirectorySecurity1()
//        {
//            char f = '/';
//            char b = '\\';
//            var rezult = true;
//            rezult = MahCoadz.Util.Tuul.BasicPathSecuritySanity(""); Assert.AreEqual(false, rezult);
//            rezult = MahCoadz.Util.Tuul.BasicPathSecuritySanity($"."); Assert.AreEqual(false, rezult);
//            rezult = MahCoadz.Util.Tuul.BasicPathSecuritySanity($"seomting.something"); Assert.AreEqual(true, rezult); // single dots should be allowed later in the path
//            rezult = MahCoadz.Util.Tuul.BasicPathSecuritySanity($".."); Assert.AreEqual(false, rezult);
//            rezult = MahCoadz.Util.Tuul.BasicPathSecuritySanity($"blah blah.. something"); Assert.AreEqual(false, rezult); // dual dots COULD be path traversal.. - are ARE dealing with a path only - not filenames...
//            rezult = MahCoadz.Util.Tuul.BasicPathSecuritySanity($"{f}"); Assert.AreEqual(false, rezult);
//            rezult = MahCoadz.Util.Tuul.BasicPathSecuritySanity($"seomthing{f}"); Assert.AreEqual(true, rezult); // forward slashes later on SHOULD be allowed
//            rezult = MahCoadz.Util.Tuul.BasicPathSecuritySanity($"{f}{f}"); Assert.AreEqual(false, rezult);
//            rezult = MahCoadz.Util.Tuul.BasicPathSecuritySanity($"{b}"); Assert.AreEqual(false, rezult);
//            rezult = MahCoadz.Util.Tuul.BasicPathSecuritySanity($"seomthing{b}"); Assert.AreEqual(true, rezult); // back slashes later on SHOULD be allowed
//            rezult = MahCoadz.Util.Tuul.BasicPathSecuritySanity($"{b}{b}"); Assert.AreEqual(false, rezult);
//            Assert.AreEqual(1, 1);
//        }

//        [TestMethod]
//        public void TestDirectoryList1()
//        {
//            var expectedrezult = 3;
//            // I put a  hidden file and a directory with 2 items in that folder.. normally it would have 1 file - AssemblyInfo.cs
//            var rezult = MahCoadz.Util.Tuul.GetFolderInfo("D:\\srclocal\\nbAPI1\\Util\\Properties", true, true, null, null, null);
//            Assert.AreEqual(expectedrezult, rezult.Count);
//        }

//        [TestMethod]
//        public void TestDirectoryList2()
//        {
//            // There are two users in GimmeMockUsers that this tests for
//            // to build and test this out out - run a batch that expects to have a d: drive
//            // with the contents of the batch file being: (base64 decoded of course)
//            // OiBAZWNobyBvZmYNCkBlY2hvIFNldHRpbmcgdXAgZm9yIERpcmVjdG9yeSBUZXN0aW5nDQptZCBkOlxUZXN0XzIzZjMNCmVjaG8gYm9veWEgPiBkOlxUZXN0XzIzZjNcRmlsZTEudHh0DQplY2hvIGJvb3lhID4gZDpcVGVzdF8yM2YzXEZpbGUyLmV4ZQ0KZWNobyBib295YSA+IGQ6XFRlc3RfMjNmM1xGaWxlMy5leGUNCmVjaG8gYm9veWEgPiBkOlxUZXN0XzIzZjNcRmlsZTQuYXNwDQplY2hvIGJvb3lhID4gZDpcVGVzdF8yM2YzXEZpbGU1LmFzcHgNCmVjaG8gYm9veWEgPiBkOlxUZXN0XzIzZjNcbm9ybWFsNi5wZGYNCmVjaG8gYm9veWEgPiBkOlxUZXN0XzIzZjNcaGlkZGVuNy5wZGYNCmF0dHJpYiAraCBkOlxUZXN0XzIzZjNcaGlkZGVuNy5wZGYNCm1kIGQ6XFRlc3RfMjNmM1xEMTENCm1kIGQ6XFRlc3RfMjNmM1xEMjINCmVjaG8gYm9veWEgPiBkOlxUZXN0XzIzZjNcRDExXEQxMUZpbGUxLnR4dA0KZWNobyBib295YSA+IGQ6XFRlc3RfMjNmM1xEMTFcRDExRmlsZTIuZXhlDQplY2hvIGJvb3lhID4gZDpcVGVzdF8yM2YzXEQxMVxEMTFGaWxlMy5leGUNCmVjaG8gYm9veWEgPiBkOlxUZXN0XzIzZjNcRDIyXEQyMkZpbGU0LmFzcA0KZWNobyBib295YSA+IGQ6XFRlc3RfMjNmM1xEMjJcRDIyRmlsZTUuYXNweA0KZWNobyBib295YSA+IGQ6XFRlc3RfMjNmM1xEMjJcc3VwZXJzZWNyZXQucGRmDQplY2hvIGJvb3lhID4gZDpcVGVzdF8yM2YzXEQyMlxEMjJoaWRkZW43LnBkZg0KYXR0cmliICtoIGQ6XFRlc3RfMjNmM1xkMjJcRDIyaGlkZGVuNy5wZGYNCg
//            // if it fails and the folders may have been messed with -
//            // nuke or rename that folder d:\Test_23f3, then run the batch file again
//            // just sayin'
//            List<SimpleFolderInfo> rezult = null;
//            List<IUzer> theTestUzers = MahCoadz.Uzer.UzerTools.GimmeStaticUzers(true);
//            var mylist = new List<string>();
//            // hit the exclud variations
//            //rezult = MahCoadz.Uzer.Tule.GetFolderInfoAuthenticated("f2fdwc23include", "newGUIDFromlocalstorage", "some ip etc to log", "d:\\Test_23f3", theTestUzers);
//            rezult = MahCoadz.Util.Tuul.GetFolderInfoWithAuthenticatedUzerMeta(null, theTestUzers.Find(uu => uu.uzerId == "f2fdwc23include"), "d:\\Test_23f3");
//            Assert.AreEqual(6, rezult.Count);
//            rezult = MahCoadz.Util.Tuul.GetFolderInfoWithAuthenticatedUzerMeta(null, theTestUzers.Find(uu => uu.uzerId == "f2fdwc23include"), "d:\\Test_23f3\\D11");
//            mylist = (from nn in rezult select nn.Name).ToList();
//            Assert.AreEqual(1, rezult.Count);
//            rezult = MahCoadz.Util.Tuul.GetFolderInfoWithAuthenticatedUzerMeta(null, theTestUzers.Find(uu => uu.uzerId == "f2fdwc23include"), "d:\\Test_23f3\\D22"); mylist = (from nn in rezult select nn.Name).ToList(); Assert.AreEqual(2, rezult.Count);
//            // hit the include and filename variations             
//            rezult = MahCoadz.Util.Tuul.GetFolderInfoWithAuthenticatedUzerMeta(null, theTestUzers.Find(uu => uu.uzerId == "f2fdwc23exclude"), "d:\\Test_23f3"); mylist = (from nn in rezult select nn.Name).ToList(); Assert.AreEqual(4, rezult.Count);
//            rezult = MahCoadz.Util.Tuul.GetFolderInfoWithAuthenticatedUzerMeta(null, theTestUzers.Find(uu => uu.uzerId == "f2fdwc23exclude"), "d:\\Test_23f3\\D11"); mylist = (from nn in rezult select nn.Name).ToList(); Assert.AreEqual(1, rezult.Count);
//            rezult = MahCoadz.Util.Tuul.GetFolderInfoWithAuthenticatedUzerMeta(null, theTestUzers.Find(uu => uu.uzerId == "f2fdwc23exclude"), "d:\\Test_23f3\\D22"); mylist = (from nn in rezult select nn.Name).ToList(); Assert.AreEqual(2, rezult.Count);
//            // hit the include and filename variations part 2
//            rezult = MahCoadz.Util.Tuul.GetFolderInfoWithAuthenticatedUzerMeta(null, theTestUzers.Find(uu => uu.uzerId == "f2fdwc23exclude2"), "d:\\Test_23f3"); mylist = (from nn in rezult select nn.Name).ToList(); Assert.AreEqual(4, rezult.Count);
//            rezult = MahCoadz.Util.Tuul.GetFolderInfoWithAuthenticatedUzerMeta(null, theTestUzers.Find(uu => uu.uzerId == "f2fdwc23exclude2"), "d:\\Test_23f3\\D11"); mylist = (from nn in rezult select nn.Name).ToList(); Assert.AreEqual(1, rezult.Count);
//            rezult = MahCoadz.Util.Tuul.GetFolderInfoWithAuthenticatedUzerMeta(null, theTestUzers.Find(uu => uu.uzerId == "f2fdwc23exclude2"), "d:\\Test_23f3\\D22"); mylist = (from nn in rezult select nn.Name).ToList(); Assert.AreEqual(3, rezult.Count);
//            // wrong hash
//            rezult = MahCoadz.Util.Tuul.GetFolderInfoWithAuthenticatedUzerMeta(null, theTestUzers.Find(uu => uu.uzerId == "booyawongy"), "d:\\Test_23f3"); mylist = (from nn in rezult select nn.Name).ToList(); Assert.AreEqual(1, rezult.Count);
//            Assert.AreEqual("Authorization Error", rezult[0].Name);
//            // hacky path...
//            rezult = MahCoadz.Util.Tuul.GetFolderInfoWithAuthenticatedUzerMeta(null, theTestUzers.Find(uu => uu.uzerId == "f2fdwc23exclude2"), "d:\\Test_23f3\\..\\.."); mylist = (from nn in rezult select nn.Name).ToList(); Assert.AreEqual(1, rezult.Count);
//            Assert.AreEqual("Disallowed Path", rezult[0].Name);
//            rezult = MahCoadz.Util.Tuul.GetFolderInfoWithAuthenticatedUzerMeta(null, theTestUzers.Find(uu => uu.uzerId == "f2fdwc23exclude2"), "d:\\"); mylist = (from nn in rezult select nn.Name).ToList(); Assert.AreEqual(1, rezult.Count);
//            Assert.AreEqual("Unauthorized Path", rezult[0].Name);

//            // special users
//            theTestUzers = MahCoadz.Uzer.UzerTools.GimmeStaticUzers(true, "OWJlMWQwMCBkb2N1bWVudGF0aW9uIH≈d:\\Test_23f3|true|true|.txt:.pdf:.doc:.docx:.xlsx:.epub:||assets■6H8tTIb0SVuw8sHeW≈D:\\Inetpub\\vhosts\\northberry.net\\httpdocs\\zsite_northberry_net\\calvarychoir|true|true|.txt:.pdf:.doc:.docx:.xlsx:.epub:||assets");
//            rezult = MahCoadz.Util.Tuul.GetFolderInfoWithAuthenticatedUzerMeta(null, theTestUzers.Find(uu => uu.uzerId == "OWJlMWQwMCBkb2N1bWVudGF0aW9uIH"), "d:\\Test_23f3");
//            mylist = (from nn in rezult select nn.Name).ToList();
//            Assert.AreEqual(5, rezult.Count);
//        }

//        [TestMethod]
//        public void TestGUIDfunctionality()
//        {
//            //var r1 = CryptoTool.newGUIDstring();
//            var r2 = Guid.NewGuid();
//            var r3 = KryptowTool.GUIDB64string(r2);
//            Assert.AreEqual(1, 1);
//        }
//        [TestMethod]
//        public void TestLoggit1()
//        {
//            try
//            {
//                new MahCoadz.Data.LogMgr().Loggit("booya");
//            }
//            catch (Exception ex)
//            {
//                var WhatThe = ex.Message;
//                Assert.Fail();
//            }
//        }
//    }
//}