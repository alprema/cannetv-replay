-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Apr 19, 2024 at 03:58 PM
-- Server version: 5.7.40
-- PHP Version: 8.0.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `cannetvreplay`
--
CREATE DATABASE IF NOT EXISTS `cannetvreplay` DEFAULT CHARACTER SET latin1 COLLATE latin1_general_ci;
USE `cannetvreplay`;

-- --------------------------------------------------------

--
-- Table structure for table `encounter_to_file`
--

DROP TABLE IF EXISTS `encounter_to_file`;
CREATE TABLE IF NOT EXISTS `encounter_to_file` (
  `encounter_id` int(11) NOT NULL,
  `file_index` int(11) NOT NULL,
  `file_path` text NOT NULL,
  `offset_in_seconds` int(11) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `encounter_to_file`
--

INSERT INTO `encounter_to_file` (`encounter_id`, `file_index`, `file_path`, `offset_in_seconds`) VALUES
(419, 0, 'video\\2\\A\\MVI_3962_1484391602_1484392367.mp4', 47);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
