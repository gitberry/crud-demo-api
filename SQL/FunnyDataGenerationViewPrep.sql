-- I would have loved to spend more time making this elegant - just a quick and dirty way to randomly select shtuff..
DROP VIEW IF EXISTS  FunnySongTitleComposer;
GO
CREATE VIEW FunnySongTitleComposer AS (
SELECT ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id, * 
FROM (
          SELECT Title='I Love You, You’re Perfect, Now Change',Composer='Joe DiPietro'
UNION ALL SELECT 'They Are Night Zombies!! They Are Neighbors!! They Have Come Back from the Dead!! Ahhhh!','Sufjan Stevens'
UNION ALL SELECT 'Nothing’severGOnnastandinmyway (again)','Wilco'
UNION ALL SELECT 'I’m So Miserable Without You, It’s Just Like Having You Around','Billy Walker'
UNION ALL SELECT 'I’m Full of Steak, and Cannot Dance','Sidney Gish'
UNION ALL SELECT 'Drop Kick Me Jesus (Through the GOal Posts of Life)','Bare'
UNION ALL SELECT 'My Lucky Pants Failed Me Again','Tom Rosenthal'
UNION ALL SELECT 'Let’s Generalize About Men','Crazy Ex-Girlfriend'
UNION ALL SELECT 'Ob-La-Di, Ob-La-Da','The Beatles'
UNION ALL SELECT 'I Like (the idea of) You','Tessa Violet'
UNION ALL SELECT 'Please Don’t Tell My Father That I Used His 1996 Honda Accord to Destroy the Town of Willow Grove Pennsylvania in 2002','Pet Symmetry'
UNION ALL SELECT 'Get Your Tongue Out of Your Mouth Because I’m Kissing You GOod bye','Ray Stevens'
UNION ALL SELECT 'If You Won’t Be My Number One, Number Two on You','Roger Miller'
UNION ALL SELECT 'In Heaven, There Is No Beer','Frank Yankovic'
UNION ALL SELECT 'The Eggplant That Ate ChicaGO','Norman Greenbaum'
UNION ALL SELECT 'You Can’t Roller Skate in a Buffalo Herd','Roger Miller'
UNION ALL SELECT 'MMMBop','Hanson'
UNION ALL SELECT 'Champagne for My Real Friends, Real Pain for My Sham Friends','Fall Out Boy'
UNION ALL SELECT 'Who Wrote Holden Caulfield','Green Day'
UNION ALL SELECT 'Girls Just Want to Have Lunch','Weird Al'
UNION ALL SELECT 'The Yodeling Veterinarian of the Alps','Veggie Tales'
UNION ALL SELECT 'The World’s Greatest Bowler Is the World’s Worst Anything Else','Panucci’s Pizza'
UNION ALL SELECT 'End Creditouilles','Ratatouille'
UNION ALL SELECT 'Ewe Fell for It','Zootopia'
UNION ALL SELECT 'Attack of the Radioactive Hamsters from a Planet Near Mars','Weird Al'
UNION ALL SELECT 'Itsy Bitsy Teenie Weenie Yellow Polka Dot Bikini','Brian Hyland'
UNION ALL SELECT 'GOd Must Have Spent a Little More Time on You','N’Sync'
UNION ALL SELECT 'I Did Something Weird Last Night','Jeff Rosenstock'
UNION ALL SELECT 'De Do Do Do, De Da Da Da','The Police'
UNION ALL SELECT 'Flying Microtonal Banana','King Gizzard and the Lizard Wizard'
UNION ALL SELECT 'Nobody Really Cares if You Don’t GO to the Party','Courtney Barnett'
UNION ALL SELECT 'Crippling Self Doubt and a General Lack of Self Confidence','Courtney Barnett'
UNION ALL SELECT 'Sing Me a Song with Social Significance','Harold Rome'
UNION ALL SELECT 'The Predatory Wasp of the Palisades Is Out to Get Us!','Sufjan Stevens'
UNION ALL SELECT 'You Know When the Trojans GOt That Horse and They Were Like, Yeah This Is Totally a Gift? That’s How Sure I Am','Panucci’s Pizza'
UNION ALL SELECT 'Don’t Eat the Yellow Snow','Frank Zappa'
UNION ALL SELECT 'Don’t Blame the World, It’s the DJ’s Fault','Cobra Starship'
UNION ALL SELECT 'A Detailed and Poetic Physical Threat to the Person Who Intentionally Vandalized 1994 Dodge Intrepid Behind Kate’s Apartment','Pet Symmetry'
UNION ALL SELECT 'Let’s Face It, Pal, You Don’t Need That Eye Surgery','Don Caballero'
UNION ALL SELECT 'Please Daddy Don’t Get Drunk This Christmas','John Denver'
UNION ALL SELECT 'Joy Division Oven Gloves','Half Man Half Biscuit'
UNION ALL SELECT 'Don’t You Know How Busy and Important I Am?','Tom Rosenthal'
UNION ALL SELECT 'Loop De Loop (Flip Flop Flyin’ In An Aeroplane)','The Beach Boys'
UNION ALL SELECT 'If You Love Someone, Set Them on Fire','Dead Milkmen'
UNION ALL SELECT 'You''re the Reason Our Kids Are So Ugly','Loretta Lynn and Conway Twitty'
UNION ALL SELECT 'We Hate It When Our Friends Become Successful','Morrissey'
UNION ALL SELECT 'Thank GOd And Greyhound (She''s GOne)','Roy Clark'
UNION ALL SELECT 'If You Don''t Believe I Love You, Just Ask My Wife','Gary P. Nunn'
UNION ALL SELECT 'You Take the Medicine (I''ll Take the Nurse)','William Penix'
UNION ALL SELECT 'I Wouldn''t Take Her to a Dog Fight','Charlie Walker'
UNION ALL SELECT 'She Never Told Me She Was a Mime','Weird Al'
UNION ALL SELECT 'Dogs Can Grow Beards All Over','Devil Wears Prada'
UNION ALL SELECT 'Satan Gave Me a Taco','Beck'
UNION ALL SELECT 'I''ve Been Flushed From the Bathroom of Your Heart','Johnny Cash'
UNION ALL SELECT 'All I Want From You (Is Away)','Loretta Lynn'
UNION ALL SELECT 'You Can''t Have Your Kate and Edith Too','Statler Brothers'
UNION ALL SELECT 'If the Phone Doesn''t Ring, It''s Me','Jimmy Buffett'
UNION ALL SELECT 'Drop Kick Me, Jesus (Through the GOal Post of Life)','Bobby Bare'
UNION ALL SELECT 'Our Lawyer Made Us Change The Name Of This Song So We Wouldn''t Get Sued','Fall Out Boy'
UNION ALL SELECT 'If My Nose Was Running Money (I''d Blow It All On You)','Aaron Wilburn & Mike Snider'
UNION ALL SELECT 'I''ve GOt Tears in My Ears From Lying on My Back in Bed While I Cry Over You','Homer & Jethro'
UNION ALL SELECT 'How Could You Believe Me When I Said I Loved You When You Know I''ve Been A Liar All My Life','Fred Astaire & Jane Powell'
UNION ALL SELECT 'I''d Rather Have a Bottle in Front of Me (Than a Frontal Lobotomy)','Dr. Randy Hanzlick'
UNION ALL SELECT 'I Don''t Know Whether to Kill Myself or GO Bowling','Instant Witness'
UNION ALL SELECT 'Billy Broke My Heart at Walgreens (I Cried All the Way to Sears)','Ruby Wright'
UNION ALL SELECT 'Thanks for the Killer Game of Crisco Twister','Crisco Twizlers'
UNION ALL SELECT 'Please, Daddy, Don''t Get Drunk This Christmas','John Denver'
UNION ALL SELECT 'You Can Make Me Dance, Sing, or Anything …','Rod Stewart and the Faces'
UNION ALL SELECT 'If You Can''t Live Without Me, Why Aren''t You Dead Yet?','Funny Mayday Parade'
UNION ALL SELECT 'Here''s a Quarter (Call Someone Who Cares)','Travis Tritt'
UNION ALL SELECT 'I Bought the Shoes That Just Walked Out on Me','Wynn Stewart'
UNION ALL SELECT 'She GOt the GOld Mine, and I GOt the Shaft','Jerry Reed'
UNION ALL SELECT 'Shoop Shoop Diddy Wop Cumma Cumma Wang Dang','Monte Video and the Cassettes'
UNION ALL SELECT 'Mmm mmm mmm mmm','Crash Test Dummies'
UNION ALL SELECT 'Put Your Big Toe in the Milk of Human Kindness','Elvis Costello'
UNION ALL SELECT 'Nothing''s GOnna Change My Clothes','They Might Be Giants'
UNION ALL SELECT 'Too Much Month at the End of the Money','Billy Hall'
UNION ALL SELECT 'My Uncle Used to Love Me, But She Died','Roger Miller'
UNION ALL SELECT 'I Wanna Find a Woman That''ll Hold My Big Toe Till I Have to GO','Captain Beefheart & the Magic Band'
UNION ALL SELECT 'Thank You (Falettinme Be Mice Elf Agin)','Sly and the Family Stone'
UNION ALL SELECT 'This Song Has No Title','Elton John'
UNION ALL SELECT 'Thanks to the Cathouse (I''m in the Doghouse With You)','Johnny Paycheck'
UNION ALL SELECT 'What Made Milwaukee Famous (Has Made a Loser Out of Me)','Jerry Lee Lewis'
UNION ALL SELECT 'If You Won''t Leave Me, I''ll Find Somebody Who Will','Warren Zevon'
) x 
)
GO
SELECT * FROM FunnySongTitleComposer;
GO
DROP VIEW IF EXISTS FunnyCompany
GO
CREATE VIEW FunnyCompany AS 
SELECT ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id, * 
FROM (
SELECT CorpName = 'COOMBS MILLIVOLT INC.' UNION ALL 
SELECT 'FORLORN CLERGIES LTD.' UNION ALL 
SELECT 'MANIPULATOR INTROMIT INC.' UNION ALL 
SELECT 'KIDNAPS COMMEMORATE INC.' UNION ALL 
SELECT 'CLERGIES CONDUCIVENESS CORP' UNION ALL 
SELECT 'FROWSIER LOWNESSES CORP' UNION ALL 
SELECT 'SLOPPIER MEDIUMISTIC LTD.' UNION ALL 
SELECT 'BATHES ASSERTED CORP' UNION ALL 
SELECT 'METRIFY ARTIFICER INC.' UNION ALL 
SELECT 'STREAKER SIGHTER INC.' UNION ALL 
SELECT 'VOLUPTUOUSNESS PROPS LTD.' UNION ALL 
SELECT 'CORSES TOXICAL CORP' UNION ALL 
SELECT 'STALINISTS DYSTROPHIES CORP' UNION ALL 
SELECT 'EXEGETES GORALS CORP' UNION ALL 
SELECT 'EXCRESCENCES MISQUOTE LTD.' UNION ALL 
SELECT 'EXTOLLERS MOPISHLY LTD.' UNION ALL 
SELECT 'PINNER TOREROS LTD.' UNION ALL 
SELECT 'SCHLEPPS UNPLEASANTLY CORP' UNION ALL 
SELECT 'STARS BLUESMAN INC.' UNION ALL 
SELECT 'NONMILITARY SUBLIMINALLY LTD.' UNION ALL 
SELECT 'RECORDABLE ANTINOMIES LTD.' UNION ALL 
SELECT 'OVERELABORATE LORIES LTD.' UNION ALL 
SELECT 'BIOFEEDBACK SUFFER INC.' UNION ALL 
SELECT 'SLITHER DEVIATED LTD.' UNION ALL 
SELECT 'JACQUARDS MYNHEER INC.' UNION ALL 
SELECT 'SESSIONAL VAUDEVILLE INC.' UNION ALL 
SELECT 'ENSILES BRAVENESS CORP' UNION ALL 
SELECT 'WANTAGE MARBLING INC.' UNION ALL 
SELECT 'UPTAKE DESPICABLY LTD.' UNION ALL 
SELECT 'DEVOUR STOATS CORP' UNION ALL 
SELECT 'ENCIPHER MELANIZED CORP' UNION ALL 
SELECT 'ONRUSH RELICT CORP' UNION ALL 
SELECT 'ALLEVIATIVE ASTRINGENT LTD.' UNION ALL 
SELECT 'PRATER TEAMWORKS LTD.' UNION ALL 
SELECT 'THROMBOSIS ORDERINGS CORP' UNION ALL 
SELECT 'CAMPCRAFT TYPESCRIPTS INC.' UNION ALL 
SELECT 'PROVISION CHANTRIES CORP' UNION ALL 
SELECT 'BREZHNEV SPECTROSCOPIC INC.' UNION ALL 
SELECT 'AEROLITH APPLIQUEING INC.' UNION ALL 
SELECT 'CURDLER STAPLE CORP' UNION ALL 
SELECT 'ARMLETS NATIONALLY INC.' UNION ALL 
SELECT 'NETTLIER LOAMIEST CORP' UNION ALL 
SELECT 'PINOLES CATEGORIZES INC.' UNION ALL 
SELECT 'IRIDESCENCE MONOGAMIES LTD.' UNION ALL 
SELECT 'LIMBERLY AESTIVATED LTD.' UNION ALL 
SELECT 'REDEVELOPED ANNELID CORP' UNION ALL 
SELECT 'BEWITCHING HANDICRAFTSMAN LTD.' UNION ALL 
SELECT 'RETURNABLE ADJUDICATES CORP' UNION ALL 
SELECT 'VALUTA URDS CORP' UNION ALL 
SELECT 'ATTIRING SENSITIVE CORP' UNION ALL 
SELECT 'LANYARD TELEGRAPHIST CORP' UNION ALL 
SELECT 'COGNIZERS AGED LTD.' UNION ALL 
SELECT 'MARBLING ROCHESTER INC.' UNION ALL 
SELECT 'RECREANCE PLENUM LTD.' UNION ALL 
SELECT 'UNRIDDLING MANNERLINESS INC.' UNION ALL 
SELECT 'ERISTIC BUSTLERS CORP' UNION ALL 
SELECT 'ASTRINGENT MASONED INC.' UNION ALL 
SELECT 'TEA ROUNDWORM CORP' UNION ALL 
SELECT 'ROPE DISSEVERING CORP' UNION ALL 
SELECT 'THESPIANS ALUMINIZED LTD.' UNION ALL 
SELECT 'REGALITIES DUALISTS INC.' UNION ALL 
SELECT 'CAMBER RESONATING INC.' UNION ALL 
SELECT 'OBLIVION BIRDBATHS CORP' UNION ALL 
SELECT 'FIBERGLASS LEVITY INC.' UNION ALL 
SELECT 'VALLEYS ILLIMITABLE CORP' UNION ALL 
SELECT 'SELECT MOOTS LTD.' UNION ALL 
SELECT 'PLANARITY SLIDEWAY LTD.' UNION ALL 
SELECT 'GORALS GORILLAS LTD.' UNION ALL 
SELECT 'GAUCHENESS OPIUM INC.' UNION ALL 
SELECT 'UNREALITY CAPTIVITY INC.' UNION ALL 
SELECT 'TEAMWORKS MELTS LTD.' UNION ALL 
SELECT 'SEWN BEDAUBING INC.' UNION ALL 
SELECT 'AGRICULTURISTS CYTOLOGIC INC.' UNION ALL 
SELECT 'HANDICRAFTSMAN EMBELLISHMENTS LTD.'
) x
GO
SELECT * FROM FunnyCompany 
GO;
DROP VIEW IF EXISTS FunnyUserName;
GO
CREATE VIEW FunnyUserName AS 
SELECT ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id, * FROM (
          SELECT UserName = 'Arthur Dent'
UNION ALL SELECT 'Ford Prefect'
UNION ALL SELECT 'Zaphod Beeblebrox'
UNION ALL SELECT 'Slarti Bartfast'
UNION ALL SELECT 'Blart Versenwald'
UNION ALL SELECT 'Eccentrica Gallumbits'
UNION ALL SELECT 'GOogleplex Starthinker'
UNION ALL SELECT 'Hig Hurtenflurst'
UNION ALL SELECT 'Hotblack Desiato'
UNION ALL SELECT 'Hurling Frootmig'
UNION ALL SELECT 'Lajestic Vantrashell'
UNION ALL SELECT 'Old Thrashbarg'
UNION ALL SELECT 'Prostetnic VoGOn Jeltz'
UNION ALL SELECT 'Questular Rontok'
UNION ALL SELECT 'Stavro Mueller'
UNION ALL SELECT 'Trin Tragula'
UNION ALL SELECT 'Veet Voojagig'
UNION ALL SELECT 'Yooden Vranx'
UNION ALL SELECT 'Zarni Woop'
) x
GO
SELECT * FROM FunnyUserName
GO 
DROP VIEW IF EXISTS FunnyStyle;
GO
CREATE VIEW FunnyStyle AS 
SELECT ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id, * FROM (
          SELECT StyleName = 'Classical'
UNION ALL SELECT 'Modern/Rock'
UNION ALL SELECT 'Country/Bluegrass'
UNION ALL SELECT 'Jazz/Blues'
UNION ALL SELECT 'Celtic/Folk'
UNION ALL SELECT 'Trance/Thrash/Dance'
UNION ALL SELECT 'Rap/Trap/Ska'
) x
GO
SELECT * FROM FunnyStyle
