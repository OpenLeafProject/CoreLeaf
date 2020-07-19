-- --------------------------------------------------------
-- Host:                         localhost
-- Versión del servidor:         10.4.13-MariaDB - mariadb.org binary distribution
-- SO del servidor:              Win64
-- HeidiSQL Versión:             11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Volcando estructura para tabla leaf.api_log
DROP TABLE IF EXISTS `api_log`;
CREATE TABLE IF NOT EXISTS `api_log` (
  `id` varchar(32) COLLATE utf8mb4_spanish_ci NOT NULL,
  `method` varchar(10) COLLATE utf8mb4_spanish_ci NOT NULL,
  `scheme` varchar(10) COLLATE utf8mb4_spanish_ci NOT NULL,
  `host` varchar(255) COLLATE utf8mb4_spanish_ci NOT NULL,
  `path` varchar(1024) COLLATE utf8mb4_spanish_ci NOT NULL,
  `querystring` longtext COLLATE utf8mb4_spanish_ci NOT NULL DEFAULT '' CHECK (json_valid(`querystring`)),
  `remoteipadress` varchar(15) COLLATE utf8mb4_spanish_ci NOT NULL,
  `response` longtext COLLATE utf8mb4_spanish_ci NOT NULL DEFAULT '' CHECK (json_valid(`response`)),
  `headers` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL DEFAULT '' CHECK (json_valid(`headers`)),
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.api_log: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `api_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `api_log` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.appointments
DROP TABLE IF EXISTS `appointments`;
CREATE TABLE IF NOT EXISTS `appointments` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `datetime` date NOT NULL,
  `forced` int(11) NOT NULL,
  `allowinvoice` int(11) NOT NULL,
  `price` int(11) DEFAULT NULL,
  `duration` int(11) NOT NULL,
  `visitmode` int(11) NOT NULL,
  `note` longtext COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `patientid` int(11) DEFAULT NULL,
  `visittypeid` int(11) NOT NULL,
  `visitmodeid` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `patientid` (`patientid`),
  KEY `visittypeid` (`visittypeid`),
  KEY `visitmodeid` (`visitmodeid`),
  CONSTRAINT `appointments_ibfk_1` FOREIGN KEY (`patientid`) REFERENCES `patients` (`nhc`),
  CONSTRAINT `appointments_ibfk_2` FOREIGN KEY (`visittypeid`) REFERENCES `visit_types` (`id`),
  CONSTRAINT `appointments_ibfk_3` FOREIGN KEY (`visitmodeid`) REFERENCES `visit_modes` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.appointments: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `appointments` DISABLE KEYS */;
/*!40000 ALTER TABLE `appointments` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.centers
DROP TABLE IF EXISTS `centers`;
CREATE TABLE IF NOT EXISTS `centers` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) COLLATE utf8mb4_spanish_ci NOT NULL,
  `name` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `NIF` varchar(10) COLLATE utf8mb4_spanish_ci NOT NULL,
  `address` varchar(255) COLLATE utf8mb4_spanish_ci NOT NULL,
  `city` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `pc` varchar(10) COLLATE utf8mb4_spanish_ci NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `code` (`code`),
  UNIQUE KEY `NIF` (`NIF`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.centers: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `centers` DISABLE KEYS */;
INSERT INTO `centers` (`id`, `code`, `name`, `NIF`, `address`, `city`, `pc`) VALUES
	(1, 'TEST', 'Test Center', '0000000000', 'Somplace', 'Tarragona', '43002');
/*!40000 ALTER TABLE `centers` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.invoices
DROP TABLE IF EXISTS `invoices`;
CREATE TABLE IF NOT EXISTS `invoices` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ammount` float NOT NULL,
  `iva` float NOT NULL,
  `invoicedate` date NOT NULL,
  `paymentdate` date NOT NULL,
  `patienid` int(11) NOT NULL,
  `appointmentid` int(11) NOT NULL,
  `centerid` int(11) NOT NULL,
  `paymentmethodid` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `patienid` (`patienid`),
  KEY `appointmentid` (`appointmentid`),
  KEY `centerid` (`centerid`),
  KEY `paymentmethodid` (`paymentmethodid`),
  CONSTRAINT `invoices_ibfk_1` FOREIGN KEY (`patienid`) REFERENCES `patients` (`nhc`),
  CONSTRAINT `invoices_ibfk_2` FOREIGN KEY (`appointmentid`) REFERENCES `appointments` (`id`),
  CONSTRAINT `invoices_ibfk_3` FOREIGN KEY (`centerid`) REFERENCES `centers` (`id`),
  CONSTRAINT `invoices_ibfk_4` FOREIGN KEY (`paymentmethodid`) REFERENCES `payment_method` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.invoices: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `invoices` DISABLE KEYS */;
/*!40000 ALTER TABLE `invoices` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.invoice_detail
DROP TABLE IF EXISTS `invoice_detail`;
CREATE TABLE IF NOT EXISTS `invoice_detail` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `concept` varchar(255) COLLATE utf8mb4_spanish_ci NOT NULL,
  `value` float NOT NULL,
  `invoiceid` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `invoiceid` (`invoiceid`),
  CONSTRAINT `invoice_detail_ibfk_1` FOREIGN KEY (`invoiceid`) REFERENCES `invoices` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.invoice_detail: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `invoice_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `invoice_detail` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.patients
DROP TABLE IF EXISTS `patients`;
CREATE TABLE IF NOT EXISTS `patients` (
  `nhc` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `surname` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `lastname` varchar(50) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `dni` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `phone` varchar(15) COLLATE utf8mb4_spanish_ci NOT NULL,
  `phonealt` varchar(15) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `address` varchar(255) COLLATE utf8mb4_spanish_ci NOT NULL,
  `city` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `pc` varchar(10) COLLATE utf8mb4_spanish_ci NOT NULL,
  `sex` int(11) NOT NULL,
  `note` varchar(255) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `lastaccess` date DEFAULT NULL,
  `email` varchar(255) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `centerid` int(11) NOT NULL,
  PRIMARY KEY (`nhc`),
  UNIQUE KEY `dni` (`dni`),
  KEY `centerid` (`centerid`),
  CONSTRAINT `patients_ibfk_1` FOREIGN KEY (`centerid`) REFERENCES `centers` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.patients: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
/*!40000 ALTER TABLE `patients` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.payment_method
DROP TABLE IF EXISTS `payment_method`;
CREATE TABLE IF NOT EXISTS `payment_method` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `code` varchar(5) COLLATE utf8mb4_spanish_ci NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `code` (`code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.payment_method: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `payment_method` DISABLE KEYS */;
/*!40000 ALTER TABLE `payment_method` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.reports
DROP TABLE IF EXISTS `reports`;
CREATE TABLE IF NOT EXISTS `reports` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `content` longtext COLLATE utf8mb4_spanish_ci NOT NULL DEFAULT '' CHECK (json_valid(`content`)),
  `creationdate` date NOT NULL,
  `signdate` date DEFAULT NULL,
  `patientid` int(11) NOT NULL,
  `appointmentid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `patientid` (`patientid`),
  KEY `appointmentid` (`appointmentid`),
  CONSTRAINT `reports_ibfk_1` FOREIGN KEY (`patientid`) REFERENCES `patients` (`nhc`),
  CONSTRAINT `reports_ibfk_2` FOREIGN KEY (`appointmentid`) REFERENCES `appointments` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.reports: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `reports` DISABLE KEYS */;
/*!40000 ALTER TABLE `reports` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.users
DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(20) COLLATE utf8mb4_spanish_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb4_spanish_ci NOT NULL,
  `name` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `surname` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `lastname` varchar(50) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `colnum` varchar(15) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `email` varchar(255) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `creationdate` date NOT NULL,
  `active` int(11) NOT NULL,
  `profileimage` varchar(255) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `lastaccess` date DEFAULT NULL,
  `lasipaccess` varchar(15) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `profileid` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`),
  KEY `profileid` (`profileid`),
  CONSTRAINT `users_ibfk_1` FOREIGN KEY (`profileid`) REFERENCES `user_profiles` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.users: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` (`id`, `username`, `password`, `name`, `surname`, `lastname`, `colnum`, `email`, `creationdate`, `active`, `profileimage`, `lastaccess`, `lasipaccess`, `profileid`) VALUES
	(4, 'admin', '21232f297a57a5a743894a0e4a801fc3', 'Admin', 'admin', NULL, NULL, NULL, '0000-00-00', 1, NULL, NULL, NULL, 1);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.users_centers
DROP TABLE IF EXISTS `users_centers`;
CREATE TABLE IF NOT EXISTS `users_centers` (
  `userid` int(11) NOT NULL,
  `centerid` int(11) NOT NULL,
  PRIMARY KEY (`userid`,`centerid`),
  KEY `centerid` (`centerid`),
  CONSTRAINT `users_centers_ibfk_1` FOREIGN KEY (`userid`) REFERENCES `users` (`id`),
  CONSTRAINT `users_centers_ibfk_2` FOREIGN KEY (`centerid`) REFERENCES `centers` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.users_centers: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `users_centers` DISABLE KEYS */;
INSERT INTO `users_centers` (`userid`, `centerid`) VALUES
	(4, 1);
/*!40000 ALTER TABLE `users_centers` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.user_profiles
DROP TABLE IF EXISTS `user_profiles`;
CREATE TABLE IF NOT EXISTS `user_profiles` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(255) COLLATE utf8mb4_spanish_ci NOT NULL,
  `code` varchar(5) COLLATE utf8mb4_spanish_ci NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `code` (`code`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.user_profiles: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `user_profiles` DISABLE KEYS */;
INSERT INTO `user_profiles` (`id`, `description`, `code`) VALUES
	(1, 'Root User', 'ROOT');
/*!40000 ALTER TABLE `user_profiles` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.visit_modes
DROP TABLE IF EXISTS `visit_modes`;
CREATE TABLE IF NOT EXISTS `visit_modes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `code` varchar(5) COLLATE utf8mb4_spanish_ci NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `code` (`code`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.visit_modes: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `visit_modes` DISABLE KEYS */;
INSERT INTO `visit_modes` (`id`, `description`, `code`) VALUES
	(1, 'Videocall', 'VIDEO');
/*!40000 ALTER TABLE `visit_modes` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.visit_types
DROP TABLE IF EXISTS `visit_types`;
CREATE TABLE IF NOT EXISTS `visit_types` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `code` varchar(5) COLLATE utf8mb4_spanish_ci NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `code` (`code`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.visit_types: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `visit_types` DISABLE KEYS */;
INSERT INTO `visit_types` (`id`, `description`, `code`) VALUES
	(1, 'Evaluation', 'EVAL');
/*!40000 ALTER TABLE `visit_types` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
