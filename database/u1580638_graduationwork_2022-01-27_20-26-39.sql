-- MariaDB dump 10.19  Distrib 10.5.10-MariaDB, for Win64 (AMD64)
--
-- Host: localhost    Database: u1580638_graduationwork
-- ------------------------------------------------------
-- Server version	10.5.10-MariaDB-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `acces_level`
--

DROP TABLE IF EXISTS `acces_level`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `acces_level` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `description` varchar(255) NOT NULL DEFAULT '',
  `acces_name` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `acces_level`
--

LOCK TABLES `acces_level` WRITE;
/*!40000 ALTER TABLE `acces_level` DISABLE KEYS */;
INSERT INTO `acces_level` VALUES (1,'Администратор','Администратор'),(2,'Пользователь','Пользователь');
/*!40000 ALTER TABLE `acces_level` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary table structure for view `authorization`
--

DROP TABLE IF EXISTS `authorization`;
/*!50001 DROP VIEW IF EXISTS `authorization`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `authorization` (
  `login` tinyint NOT NULL,
  `password` tinyint NOT NULL,
  `acces_name` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `city`
--

DROP TABLE IF EXISTS `city`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `city` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `id_name_region` int(11) unsigned NOT NULL,
  `name_city` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`),
  KEY `FK_city_region_id` (`id_name_region`),
  CONSTRAINT `FK_city_region_id` FOREIGN KEY (`id_name_region`) REFERENCES `region` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `city`
--

LOCK TABLES `city` WRITE;
/*!40000 ALTER TABLE `city` DISABLE KEYS */;
INSERT INTO `city` VALUES (1,1,'Брянск'),(2,2,'Брянск');
/*!40000 ALTER TABLE `city` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `diagnosis`
--

DROP TABLE IF EXISTS `diagnosis`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `diagnosis` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name_of_diagnosis` varchar(255) NOT NULL DEFAULT '',
  `id_type_diagnosis` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_diagnosis_type_diagnosis_id` (`id_type_diagnosis`),
  CONSTRAINT `FK_diagnosis_type_diagnosis_id` FOREIGN KEY (`id_type_diagnosis`) REFERENCES `type_diagnosis` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `diagnosis`
--

LOCK TABLES `diagnosis` WRITE;
/*!40000 ALTER TABLE `diagnosis` DISABLE KEYS */;
INSERT INTO `diagnosis` VALUES (1,'Грипп',1),(2,'Тест',2);
/*!40000 ALTER TABLE `diagnosis` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `district`
--

DROP TABLE IF EXISTS `district`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `district` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `id_name_city` int(11) unsigned NOT NULL,
  `name_district` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`),
  KEY `FK_district_city_id` (`id_name_city`),
  CONSTRAINT `FK_district_city_id` FOREIGN KEY (`id_name_city`) REFERENCES `city` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `district`
--

LOCK TABLES `district` WRITE;
/*!40000 ALTER TABLE `district` DISABLE KEYS */;
INSERT INTO `district` VALUES (1,1,'1'),(2,2,'2');
/*!40000 ALTER TABLE `district` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employees`
--

DROP TABLE IF EXISTS `employees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `employees` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `surname` varchar(255) NOT NULL DEFAULT '',
  `name` varchar(255) NOT NULL DEFAULT '',
  `patronymic` varchar(255) NOT NULL DEFAULT '',
  `id_post` int(11) unsigned NOT NULL,
  `phone_number` varchar(17) NOT NULL DEFAULT '',
  `gender` varchar(7) NOT NULL DEFAULT '',
  `education` varchar(255) NOT NULL DEFAULT '',
  `id_user` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_employees_user_id` (`id_user`),
  KEY `FK_employees_post_id` (`id_post`),
  CONSTRAINT `FK_employees_post_id` FOREIGN KEY (`id_post`) REFERENCES `post` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_employees_user_id` FOREIGN KEY (`id_user`) REFERENCES `user` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employees`
--

LOCK TABLES `employees` WRITE;
/*!40000 ALTER TABLE `employees` DISABLE KEYS */;
INSERT INTO `employees` VALUES (1,'Савичев','Вадим','Викторович',1,'444','Мужчина','Высшее',1),(2,'Фролов','Олег','Генадьевич',2,'321','Мужчина','Высшее',2);
/*!40000 ALTER TABLE `employees` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `medical_institution`
--

DROP TABLE IF EXISTS `medical_institution`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `medical_institution` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name_institution` varchar(255) NOT NULL DEFAULT '',
  `id_type_of_institution` int(11) unsigned NOT NULL,
  `id_distrtict` int(11) unsigned NOT NULL,
  `id_statistics_diagnosis` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_medical_institution_type_of_institution_id` (`id_type_of_institution`),
  KEY `FK_medical_institution_district_id` (`id_distrtict`),
  KEY `FK_medical_institution_statistics_diagnosis_id` (`id_statistics_diagnosis`),
  CONSTRAINT `FK_medical_institution_district_id` FOREIGN KEY (`id_distrtict`) REFERENCES `district` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_medical_institution_statistics_diagnosis_id` FOREIGN KEY (`id_statistics_diagnosis`) REFERENCES `statistics_diagnosis` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_medical_institution_type_of_institution_id` FOREIGN KEY (`id_type_of_institution`) REFERENCES `type_of_institution` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `medical_institution`
--

LOCK TABLES `medical_institution` WRITE;
/*!40000 ALTER TABLE `medical_institution` DISABLE KEYS */;
INSERT INTO `medical_institution` VALUES (1,'Больница 5',1,1,1),(2,'Больница 4',2,2,2);
/*!40000 ALTER TABLE `medical_institution` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `population_group`
--

DROP TABLE IF EXISTS `population_group`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `population_group` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `age` varchar(5) NOT NULL DEFAULT '',
  `gender` varchar(7) NOT NULL DEFAULT '',
  `disability` varchar(3) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `population_group`
--

LOCK TABLES `population_group` WRITE;
/*!40000 ALTER TABLE `population_group` DISABLE KEYS */;
INSERT INTO `population_group` VALUES (1,'15-25','Мужчина','Нет'),(2,'15-25','Мужчина','Да');
/*!40000 ALTER TABLE `population_group` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `post`
--

DROP TABLE IF EXISTS `post`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `post` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name_post` varchar(255) NOT NULL DEFAULT '',
  `wages` float NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `post`
--

LOCK TABLES `post` WRITE;
/*!40000 ALTER TABLE `post` DISABLE KEYS */;
INSERT INTO `post` VALUES (1,'Администратор',50),(2,'Пользователь',40);
/*!40000 ALTER TABLE `post` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `post_acces_level`
--

DROP TABLE IF EXISTS `post_acces_level`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `post_acces_level` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `id_post` int(11) unsigned NOT NULL,
  `id_acces_level` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_position_acces_level_acces_level_id` (`id_acces_level`),
  CONSTRAINT `FK_position_acces_level_acces_level_id` FOREIGN KEY (`id_acces_level`) REFERENCES `acces_level` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_post_acces_level_post_id` FOREIGN KEY (`id`) REFERENCES `post` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `post_acces_level`
--

LOCK TABLES `post_acces_level` WRITE;
/*!40000 ALTER TABLE `post_acces_level` DISABLE KEYS */;
INSERT INTO `post_acces_level` VALUES (1,1,1),(2,2,2);
/*!40000 ALTER TABLE `post_acces_level` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `region`
--

DROP TABLE IF EXISTS `region`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `region` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name_region` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `region`
--

LOCK TABLES `region` WRITE;
/*!40000 ALTER TABLE `region` DISABLE KEYS */;
INSERT INTO `region` VALUES (1,'Фокинский'),(2,'Советский');
/*!40000 ALTER TABLE `region` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `statistics_diagnosis`
--

DROP TABLE IF EXISTS `statistics_diagnosis`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `statistics_diagnosis` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `number_of_people` int(11) unsigned NOT NULL,
  `id_diagnosis` int(11) unsigned NOT NULL,
  `id_population_group` int(11) unsigned NOT NULL,
  `datetime_period` datetime NOT NULL,
  `id_district` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_statistics_diagnosis_diagnosis_id` (`id_diagnosis`),
  KEY `FK_statistics_diagnosis_population_group_id` (`id_population_group`),
  KEY `FK_statistics_diagnosis_district_id` (`id_district`),
  CONSTRAINT `FK_statistics_diagnosis_diagnosis_id` FOREIGN KEY (`id_diagnosis`) REFERENCES `diagnosis` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_statistics_diagnosis_district_id` FOREIGN KEY (`id_district`) REFERENCES `district` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_statistics_diagnosis_population_group_id` FOREIGN KEY (`id_population_group`) REFERENCES `population_group` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `statistics_diagnosis`
--

LOCK TABLES `statistics_diagnosis` WRITE;
/*!40000 ALTER TABLE `statistics_diagnosis` DISABLE KEYS */;
INSERT INTO `statistics_diagnosis` VALUES (1,50,1,1,'2022-01-27 00:00:00',1),(2,10,2,2,'2022-01-19 00:00:00',2);
/*!40000 ALTER TABLE `statistics_diagnosis` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `type_diagnosis`
--

DROP TABLE IF EXISTS `type_diagnosis`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `type_diagnosis` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name_type_of_diagnosis` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `type_diagnosis`
--

LOCK TABLES `type_diagnosis` WRITE;
/*!40000 ALTER TABLE `type_diagnosis` DISABLE KEYS */;
INSERT INTO `type_diagnosis` VALUES (1,'Грипп'),(2,'Тест');
/*!40000 ALTER TABLE `type_diagnosis` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `type_of_institution`
--

DROP TABLE IF EXISTS `type_of_institution`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `type_of_institution` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name_type` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `type_of_institution`
--

LOCK TABLES `type_of_institution` WRITE;
/*!40000 ALTER TABLE `type_of_institution` DISABLE KEYS */;
INSERT INTO `type_of_institution` VALUES (1,'Частная'),(2,'Автономная');
/*!40000 ALTER TABLE `type_of_institution` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `login` varchar(255) NOT NULL DEFAULT '',
  `password` varchar(255) NOT NULL DEFAULT '',
  `e-mail` varchar(255) NOT NULL DEFAULT '',
  `id_medical_institution` int(11) unsigned NOT NULL,
  `id_district` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_user_medical_institution_id` (`id_medical_institution`),
  KEY `FK_user_district_id` (`id_district`),
  CONSTRAINT `FK_user_district_id` FOREIGN KEY (`id_district`) REFERENCES `district` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_user_medical_institution_id` FOREIGN KEY (`id_medical_institution`) REFERENCES `medical_institution` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'admin','admin','444',1,1),(2,'user','user','444',2,2);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary table structure for view `users`
--

DROP TABLE IF EXISTS `users`;
/*!50001 DROP VIEW IF EXISTS `users`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `users` (
  `login` tinyint NOT NULL,
  `password` tinyint NOT NULL,
  `surname` tinyint NOT NULL,
  `name` tinyint NOT NULL,
  `name_post` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Dumping routines for database 'u1580638_graduationwork'
--

--
-- Final view structure for view `authorization`
--

/*!50001 DROP TABLE IF EXISTS `authorization`*/;
/*!50001 DROP VIEW IF EXISTS `authorization`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`u1580638_learner`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `authorization` AS select `user`.`login` AS `login`,`user`.`password` AS `password`,`acces_level`.`acces_name` AS `acces_name` from ((((`employees` join `user` on(`employees`.`id_user` = `user`.`id`)) join `post` on(`employees`.`id_post` = `post`.`id`)) join `post_acces_level` on(`post_acces_level`.`id` = `post`.`id`)) join `acces_level` on(`post_acces_level`.`id_acces_level` = `acces_level`.`id`)) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `users`
--

/*!50001 DROP TABLE IF EXISTS `users`*/;
/*!50001 DROP VIEW IF EXISTS `users`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`u1580638_learner`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `users` AS select `user`.`login` AS `login`,`user`.`password` AS `password`,`employees`.`surname` AS `surname`,`employees`.`name` AS `name`,`post`.`name_post` AS `name_post` from ((`employees` join `user` on(`employees`.`id_user` = `user`.`id`)) join `post` on(`employees`.`id_post` = `post`.`id`)) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-01-27 20:26:43
