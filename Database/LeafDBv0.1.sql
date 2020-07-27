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
  `querystring` longtext COLLATE utf8mb4_spanish_ci DEFAULT '',
  `remoteipadress` varchar(15) COLLATE utf8mb4_spanish_ci NOT NULL,
  `response` longtext COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `headers` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL DEFAULT '' CHECK (json_valid(`headers`)),
  `insertdate` datetime DEFAULT NULL,
  `updatedate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.api_log: ~8 rows (aproximadamente)
/*!40000 ALTER TABLE `api_log` DISABLE KEYS */;
INSERT INTO `api_log` (`id`, `method`, `scheme`, `host`, `path`, `querystring`, `remoteipadress`, `response`, `headers`, `insertdate`, `updatedate`) VALUES
	('2a9272d7c3f1c675ec89aceee0c13b16', 'POST', 'https', 'localhost:44371', '/api/v0.1/patient/save', '"{    "id": 1,    "name": "Chucky",    "surname": "Norrys",    "lastname": "",    "dni": "31231223",    "phone": "33333",    "phoneAlt": "",    "address": "noplace",    "city": "nocity",    "pc": "43002",    "sex": 1,    "note": "",    "lastAccess": "2020-07-20T00:00:00",    "email": "",    "bornDate": "2020-07-20T00:00:00",    "center": 1}"', '::1', '{"error":"Cannot save patient without setted NHC"}', '{"Accept":["*/*"],"Accept-Encoding":["gzip, deflate, br"],"Connection":["keep-alive"],"Content-Length":["375"],"Content-Type":["application/json"],"Host":["localhost:44371"],"User-Agent":["PostmanRuntime/7.26.1"],"token":["eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6IlJPT1QiLCJuYmYiOjE1OTU0MTA5NzIsImV4cCI6MTU5NTQzMjU3MiwiaWF0IjoxNTk1NDEwOTcyfQ.1BRQmWSZyt4ZM3sW7-ajnnUXGwcFdXF1evUdZACk7aA"],"Postman-Token":["cfa14f24-bcf9-4c72-a39a-d4a127fa8783"]}', '2020-07-22 14:14:36', '2020-07-22 14:14:36'),
	('3310e97e2937f787212d741bf81f85ef', 'POST', 'https', 'localhost:44371', '/api/v0.1/center/save', '"{    "id": 4,    "code": "TEST2",    "name": "Test Cente 2r",    "nif": "000000000C",    "address": "Somplace x2",    "city": "Tarragona",    "pc": "43002",    "creationDate": "2020-07-20T14:27:43"} "', '::1', NULL, '{"Accept":["*/*"],"Accept-Encoding":["gzip, deflate, br"],"Connection":["keep-alive"],"Content-Length":["217"],"Content-Type":["application/json"],"Host":["localhost:44371"],"User-Agent":["PostmanRuntime/7.26.1"],"token":["eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6IlJPT1QiLCJuYmYiOjE1OTU0MTA5NzIsImV4cCI6MTU5NTQzMjU3MiwiaWF0IjoxNTk1NDEwOTcyfQ.1BRQmWSZyt4ZM3sW7-ajnnUXGwcFdXF1evUdZACk7aA"],"Postman-Token":["59342334-cd40-4413-adfb-91d88a1319ff"]}', '2020-07-22 14:11:24', '2020-07-22 14:11:24'),
	('5fe78394da4dda3373089c04f04dbe21', 'POST', 'https', 'localhost:44371', '/api/v0.1/center/save', '"{    "id": 4,    "code": "TEST2",    "name": "Test Cente 2r",    "nif": "000000000C",    "address": "Somplace x2",    "city": "Tarragona",    "pc": "43002",    "creationDate": "2020-07-20T14:27:43"} "', '::1', NULL, '{"Accept":["*/*"],"Accept-Encoding":["gzip, deflate, br"],"Connection":["keep-alive"],"Content-Length":["217"],"Content-Type":["application/json"],"Host":["localhost:44371"],"User-Agent":["PostmanRuntime/7.26.1"],"token":["eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6IlJPT1QiLCJuYmYiOjE1OTU0MTA5NzIsImV4cCI6MTU5NTQzMjU3MiwiaWF0IjoxNTk1NDEwOTcyfQ.1BRQmWSZyt4ZM3sW7-ajnnUXGwcFdXF1evUdZACk7aA"],"Postman-Token":["7e83c7b1-584f-439f-a511-829c47c882cd"]}', '2020-07-22 14:10:35', '2020-07-22 14:10:35'),
	('7c090f38ab8dd8ff85cbd3f3d102a8bd', 'POST', 'https', 'localhost:44371', '/api/v0.1/patient/save', '"{    "nhc": 1,    "name": "Chucky",    "surname": "Norrys",    "lastname": "",    "dni": "31231223",    "phone": "33333",    "phoneAlt": "",    "address": "noplace",    "city": "nocity",    "pc": "43002",    "sex": 1,    "note": "",    "lastAccess": "2020-07-20T00:00:00",    "email": "",    "bornDate": "2020-07-20T00:00:00",    "center": 1}"', '::1', '{"error":"Duplicate entry \'31231223\' for key \'dni\'"}', '{"Accept":["*/*"],"Accept-Encoding":["gzip, deflate, br"],"Connection":["keep-alive"],"Content-Length":["376"],"Content-Type":["application/json"],"Host":["localhost:44371"],"User-Agent":["PostmanRuntime/7.26.1"],"token":["eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6IlJPT1QiLCJuYmYiOjE1OTU0MTA5NzIsImV4cCI6MTU5NTQzMjU3MiwiaWF0IjoxNTk1NDEwOTcyfQ.1BRQmWSZyt4ZM3sW7-ajnnUXGwcFdXF1evUdZACk7aA"],"Postman-Token":["cca83cae-dfa9-4fc6-9cbf-66912ad100c9"]}', '2020-07-22 14:14:42', '2020-07-22 14:14:42'),
	('b82915bde212986b1789e93e5d4ca8ea', 'POST', 'https', 'localhost:44371', '/api/v0.1/patient/save', '"{    "name": "Chucky",    "surname": "Norrys",    "lastname": "",    "dni": "31231223",    "phone": "33333",    "phoneAlt": "",    "address": "noplace",    "city": "nocity",    "pc": "43002",    "sex": 1,    "note": "",    "lastAccess": "2020-07-20T00:00:00",    "email": "",    "bornDate": "2020-07-20T00:00:00",    "center": 1}"', '::1', '{"error":"Cannot save patient without setted NHC"}', '{"Accept":["*/*"],"Accept-Encoding":["gzip, deflate, br"],"Connection":["keep-alive"],"Content-Length":["361"],"Content-Type":["application/json"],"Host":["localhost:44371"],"User-Agent":["PostmanRuntime/7.26.1"],"token":["eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6IlJPT1QiLCJuYmYiOjE1OTU0MTA5NzIsImV4cCI6MTU5NTQzMjU3MiwiaWF0IjoxNTk1NDEwOTcyfQ.1BRQmWSZyt4ZM3sW7-ajnnUXGwcFdXF1evUdZACk7aA"],"Postman-Token":["296edbe5-4946-4c0b-9fd6-0b3c381ac7e2"]}', '2020-07-22 14:14:23', '2020-07-22 14:14:23'),
	('c89a4eaa3d0f60125bc2cb82c4e7d673', 'POST', 'https', 'localhost:44371', '/api/v0.1/patient/save', '"{    "name": "Chucky",    "surname": "Norrys",    "lastname": "",    "dni": "31231223",    "phone": "33333",    "phoneAlt": "",    "address": "noplace",    "city": "nocity",    "pc": "43002",    "sex": 1,    "note": "",    "lastAccess": "2020-07-20T00:00:00",    "email": "",    "bornDate": "2020-07-20T00:00:00",    "center": 1}"', '::1', '{"type":"https://tools.ietf.org/html/rfc7235#section-3.1","title":"Unauthorized","status":401,"traceId":"|8bef1117-4c9e6a5c760686bc."}', '{"Accept":["*/*"],"Accept-Encoding":["gzip, deflate, br"],"Connection":["keep-alive"],"Content-Length":["361"],"Content-Type":["application/json"],"Host":["localhost:44371"],"User-Agent":["PostmanRuntime/7.26.1"],"token":["eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6IlJPT1QiLCJuYmYiOjE1OTUzMTUxMDcsImV4cCI6MTU5NTMzNjcwNywiaWF0IjoxNTk1MzE1MTA3fQ.d4U09f2QIft34L5BoU1gfUvHZf_S2o8dfrqKpcD0ZFM"],"Postman-Token":["8bd5a925-016a-41b8-96c5-f30f2a52277c"]}', '2020-07-22 14:14:07', '2020-07-22 14:14:07'),
	('da8b4d2db8738078c9a1cd7882eb76dd', 'POST', 'https', 'localhost:44371', '/api/v0.1/patient/save', '"{    "nhc": 1,    "name": "Chucky",    "surname": "Norrys",    "lastname": "",    "dni": "3123122343",    "phone": "33333",    "phoneAlt": "",    "address": "noplace",    "city": "nocity",    "pc": "43002",    "sex": 1,    "note": "",    "lastAccess": "2020-07-20T00:00:00",    "email": "",    "bornDate": "2020-07-20T00:00:00",    "center": 1}"', '::1', '{"nhc":1,"name":"Chucky","surname":"Norrys","lastname":"","dni":"3123122343","phone":"33333","phoneAlt":"","address":"noplace","city":"nocity","pc":"43002","sex":1,"note":"","lastAccess":"2020-07-20T00:00:00","email":"","bornDate":"2020-07-20T00:00:00","center":{"id":1,"code":"TEST","name":"Test Center","nif":"0000000000","address":"Somplace","city":"Tarragona","pc":"43002","creationDate":"2020-07-20T14:27:43"}}', '{"Accept":["*/*"],"Accept-Encoding":["gzip, deflate, br"],"Connection":["keep-alive"],"Content-Length":["378"],"Content-Type":["application/json"],"Host":["localhost:44371"],"User-Agent":["PostmanRuntime/7.26.1"],"token":["eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6IlJPT1QiLCJuYmYiOjE1OTU0MTA5NzIsImV4cCI6MTU5NTQzMjU3MiwiaWF0IjoxNTk1NDEwOTcyfQ.1BRQmWSZyt4ZM3sW7-ajnnUXGwcFdXF1evUdZACk7aA"],"Postman-Token":["bd31c7e4-4156-4898-aa10-dc1d7c1b5b51"]}', '2020-07-22 14:14:48', '2020-07-22 14:14:48'),
	('dbf5ee692af0e99220283f8d138125c0', 'POST', 'https', 'localhost:44371', '/api/v0.1/center/save', '"{    "id": 4,    "code": "TEST2",    "name": "Test Cente 2r",    "nif": "000000000C",    "address": "Somplace x2",    "city": "Tarragona",    "pc": "43002",    "creationDate": "2020-07-20T14:27:43"} "', '::1', '{"id":4,"code":"TEST2","name":"Test Cente 2r","nif":"000000000C","address":"Somplace x2","city":"Tarragona","pc":"43002","creationDate":"2020-07-20T14:27:43"}', '{"Accept":["*/*"],"Accept-Encoding":["gzip, deflate, br"],"Connection":["keep-alive"],"Content-Length":["217"],"Content-Type":["application/json"],"Host":["localhost:44371"],"User-Agent":["PostmanRuntime/7.26.1"],"token":["eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6IlJPT1QiLCJuYmYiOjE1OTU0MTA5NzIsImV4cCI6MTU5NTQzMjU3MiwiaWF0IjoxNTk1NDEwOTcyfQ.1BRQmWSZyt4ZM3sW7-ajnnUXGwcFdXF1evUdZACk7aA"],"Postman-Token":["09a8fc0a-a550-4cf8-9b98-8908b97e5f16"]}', '2020-07-22 14:13:41', '2020-07-22 14:13:41');
/*!40000 ALTER TABLE `api_log` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.appointments
DROP TABLE IF EXISTS `appointments`;
CREATE TABLE IF NOT EXISTS `appointments` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `datetime` datetime NOT NULL,
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
  `creationdate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `code` (`code`),
  UNIQUE KEY `NIF` (`NIF`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.centers: ~2 rows (aproximadamente)
/*!40000 ALTER TABLE `centers` DISABLE KEYS */;
INSERT INTO `centers` (`id`, `code`, `name`, `NIF`, `address`, `city`, `pc`, `creationdate`) VALUES
	(1, 'TEST', 'Test Center', '0000000000', 'Somplace', 'Tarragona', '43002', '2020-07-20 14:27:43'),
	(4, 'TEST2', 'Test Cente 2r', '000000000C', 'Somplace x2', 'Tarragona', '43002', '2020-07-20 14:27:43');
/*!40000 ALTER TABLE `centers` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.invoices
DROP TABLE IF EXISTS `invoices`;
CREATE TABLE IF NOT EXISTS `invoices` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ammount` float NOT NULL,
  `iva` float NOT NULL,
  `invoicedate` datetime NOT NULL,
  `paymentdate` datetime NOT NULL,
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
  `lastaccess` datetime DEFAULT NULL,
  `email` varchar(255) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `centerid` int(11) NOT NULL,
  `borndate` date DEFAULT NULL,
  PRIMARY KEY (`nhc`),
  UNIQUE KEY `dni` (`dni`),
  KEY `centerid` (`centerid`),
  CONSTRAINT `patients_ibfk_1` FOREIGN KEY (`centerid`) REFERENCES `centers` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=106 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.patients: ~14 rows (aproximadamente)
/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
INSERT INTO `patients` (`nhc`, `name`, `surname`, `lastname`, `dni`, `phone`, `phonealt`, `address`, `city`, `pc`, `sex`, `note`, `lastaccess`, `email`, `centerid`, `borndate`) VALUES
	(1, 'Chucky', 'Norrys', '', '3123122343', '33333', '', 'noplace', 'nocity', '43002', 1, '', '2020-07-20 00:00:00', '', 1, '2020-07-20'),
	(45, 'Chucky', 'Norrys', '', '31231223', '33333', '', 'noplace', 'nocity', '43002', 1, '', '2020-07-20 00:00:00', '', 1, '2020-07-20'),
	(46, 'Chucky', 'norry', '', '3311333X', '33333', '43543543', '', '', '', 1, '', '0001-01-01 00:00:00', '', 1, '0001-01-01'),
	(65, 'Chuck', 'Norris', '', '20847888F', '680305080', '977854258', 'Carrer del mar 5 s/n', 'Reus', '43201', 1, 'Nothing to say', '2020-07-27 10:36:05', 'chuck.norris@google.apple', 1, '4052-01-01');
/*!40000 ALTER TABLE `patients` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.payment_method
DROP TABLE IF EXISTS `payment_method`;
CREATE TABLE IF NOT EXISTS `payment_method` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `code` varchar(5) COLLATE utf8mb4_spanish_ci NOT NULL,
  `creationdate` datetime DEFAULT NULL,
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
  `creationdate` datetime NOT NULL,
  `signdate` datetime DEFAULT NULL,
  `patientid` int(11) NOT NULL,
  `appointmentid` int(11) DEFAULT NULL,
  `hash` varchar(255) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
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
  `creationdate` datetime DEFAULT NULL,
  `active` int(11) NOT NULL,
  `profileimage` varchar(255) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `lastaccess` datetime DEFAULT NULL,
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
	(4, 'admin', '21232f297a57a5a743894a0e4a801fc3', 'Admin', 'admin', NULL, NULL, NULL, '1987-05-05 01:01:01', 1, NULL, NULL, NULL, 1);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.users_centers
DROP TABLE IF EXISTS `users_centers`;
CREATE TABLE IF NOT EXISTS `users_centers` (
  `userid` int(11) NOT NULL,
  `centerid` int(11) NOT NULL,
  `creationdate` datetime DEFAULT NULL,
  PRIMARY KEY (`userid`,`centerid`),
  KEY `centerid` (`centerid`),
  CONSTRAINT `users_centers_ibfk_1` FOREIGN KEY (`userid`) REFERENCES `users` (`id`),
  CONSTRAINT `users_centers_ibfk_2` FOREIGN KEY (`centerid`) REFERENCES `centers` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.users_centers: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `users_centers` DISABLE KEYS */;
INSERT INTO `users_centers` (`userid`, `centerid`, `creationdate`) VALUES
	(4, 1, '2020-07-20 14:26:43');
/*!40000 ALTER TABLE `users_centers` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.user_profiles
DROP TABLE IF EXISTS `user_profiles`;
CREATE TABLE IF NOT EXISTS `user_profiles` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(255) COLLATE utf8mb4_spanish_ci NOT NULL,
  `code` varchar(5) COLLATE utf8mb4_spanish_ci NOT NULL,
  `creationdate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `code` (`code`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.user_profiles: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `user_profiles` DISABLE KEYS */;
INSERT INTO `user_profiles` (`id`, `description`, `code`, `creationdate`) VALUES
	(1, 'Root User', 'ROOT', '2020-07-20 14:26:40');
/*!40000 ALTER TABLE `user_profiles` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.visit_modes
DROP TABLE IF EXISTS `visit_modes`;
CREATE TABLE IF NOT EXISTS `visit_modes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `code` varchar(5) COLLATE utf8mb4_spanish_ci NOT NULL,
  `creationdate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `code` (`code`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.visit_modes: ~1 rows (aproximadamente)
/*!40000 ALTER TABLE `visit_modes` DISABLE KEYS */;
INSERT INTO `visit_modes` (`id`, `description`, `code`, `creationdate`) VALUES
	(1, 'Videocall', 'VIDEO', '2020-07-20 14:26:38');
/*!40000 ALTER TABLE `visit_modes` ENABLE KEYS */;

-- Volcando estructura para tabla leaf.visit_types
DROP TABLE IF EXISTS `visit_types`;
CREATE TABLE IF NOT EXISTS `visit_types` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `code` varchar(5) COLLATE utf8mb4_spanish_ci NOT NULL,
  `creationdate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `code` (`code`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- Volcando datos para la tabla leaf.visit_types: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `visit_types` DISABLE KEYS */;
INSERT INTO `visit_types` (`id`, `description`, `code`, `creationdate`) VALUES
	(1, 'Evaluation', 'EVAL', '2020-07-20 14:26:35');
/*!40000 ALTER TABLE `visit_types` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
