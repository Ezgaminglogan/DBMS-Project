-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 04, 2025 at 12:57 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET FOREIGN_KEY_CHECKS=0;
SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `new_activitydms`
--

-- --------------------------------------------------------

--
-- Table structure for table `course`
--

CREATE TABLE `course` (
  `Course_number` varchar(50) NOT NULL,
  `Course_name` varchar(100) DEFAULT NULL,
  `Credit_hours` int(11) DEFAULT NULL,
  `Department` varchar(100) DEFAULT NULL,
  `Year` varchar(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `course`
--

INSERT INTO `course` (`Course_number`, `Course_name`, `Credit_hours`, `Department`, `Year`) VALUES
('CC123', 'Programming 1', 5, 'College of Technology', '1st Year'),
('CCI122', 'Programming 2', 5, 'College of Technology', '2nd Year');

-- --------------------------------------------------------

--
-- Table structure for table `enrollment`
--

CREATE TABLE `enrollment` (
  `EnrollmentID` int(11) NOT NULL,
  `UserID` int(11) DEFAULT NULL,
  `CourseNumber` varchar(50) DEFAULT NULL,
  `SectionIdentifier` varchar(50) DEFAULT NULL,
  `EnrollmentDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `enrollment`
--

INSERT INTO `enrollment` (`EnrollmentID`, `UserID`, `CourseNumber`, `SectionIdentifier`, `EnrollmentDate`) VALUES
(5, 1337387, 'CC123', 'Section F', '2025-01-04 18:50:31'),
(6, 1337387, 'CCI122', 'Section F', '2025-01-04 19:15:36');

--
-- Triggers `enrollment`
--
DELIMITER $$
CREATE TRIGGER `trg_decrement_capacity` AFTER INSERT ON `enrollment` FOR EACH ROW BEGIN
    -- Check if the SectionIdentifier in the new enrollment exists in the section table
    DECLARE current_capacity INT;

    SELECT Capacity INTO current_capacity 
    FROM section 
    WHERE Section_identifier = NEW.SectionIdentifier;

    -- Ensure the capacity is greater than 0
    IF current_capacity > 0 THEN
        -- Decrement the capacity in the section table
        UPDATE section
        SET Capacity = Capacity - 1
        WHERE Section_identifier = NEW.SectionIdentifier;
    ELSE
        -- Optionally, you could throw an error or log that the section is full
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Cannot enroll: Section is full.';
    END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `gradereport`
--

CREATE TABLE `gradereport` (
  `GradeID` int(11) NOT NULL,
  `UserID` int(11) DEFAULT NULL,
  `CourseNumber` varchar(50) DEFAULT NULL,
  `Grade` char(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `prerequisite`
--

CREATE TABLE `prerequisite` (
  `PrerequisiteID` int(11) NOT NULL,
  `Course_number` varchar(50) DEFAULT NULL,
  `PrerequisiteCourse_number` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `room`
--

CREATE TABLE `room` (
  `RoomNumber` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `room`
--

INSERT INTO `room` (`RoomNumber`) VALUES
('301');

-- --------------------------------------------------------

--
-- Table structure for table `schedule`
--

CREATE TABLE `schedule` (
  `ScheduleID` int(11) NOT NULL,
  `TeacherID` int(11) DEFAULT NULL,
  `CourseNumber` varchar(50) DEFAULT NULL,
  `DayOfWeek` varchar(10) DEFAULT NULL,
  `Time` varchar(20) DEFAULT NULL,
  `RoomNumber` varchar(50) DEFAULT NULL,
  `Section` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `schedule`
--

INSERT INTO `schedule` (`ScheduleID`, `TeacherID`, `CourseNumber`, `DayOfWeek`, `Time`, `RoomNumber`, `Section`) VALUES
(1, 2, '1', 'Monday', '4:00 PM-9:00 PM', '301', 'Section F');

-- --------------------------------------------------------

--
-- Table structure for table `section`
--

CREATE TABLE `section` (
  `Section_identifier` varchar(50) NOT NULL,
  `Course_number` varchar(50) DEFAULT NULL,
  `Capacity` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `section`
--

INSERT INTO `section` (`Section_identifier`, `Course_number`, `Capacity`) VALUES
('Section F', '3', 4);

-- --------------------------------------------------------

--
-- Table structure for table `teacherlogs`
--

CREATE TABLE `teacherlogs` (
  `log_id` int(11) NOT NULL,
  `teacher_ID` int(11) NOT NULL,
  `Hours` int(11) DEFAULT NULL,
  `Time_In` datetime DEFAULT NULL,
  `Time_Out` datetime DEFAULT NULL,
  `Salary` decimal(10,0) DEFAULT NULL,
  `CreatedAT` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `UserID` int(11) NOT NULL,
  `Role` enum('Student','Teacher','Admin') NOT NULL,
  `FirstName` varchar(100) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Qualifications` enum('Part Timer','Masteral','PhD','Doctorate') DEFAULT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserID`, `Role`, `FirstName`, `LastName`, `Email`, `Password`, `Qualifications`, `CreatedAt`) VALUES
(1, 'Admin', 'Admin', 'Admin', 'admin@admin.com', '123', NULL, '2025-01-04 09:22:21'),
(2, 'Teacher', 'Logan', 'Panucat', 'logan.panucat@gmail.com', '123', 'Part Timer', '2025-01-04 09:22:52'),
(1337387, 'Student', 'Kyle', 'Ragasa', 'kyle@gmail.com', '123', NULL, '2025-01-04 09:23:53');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `course`
--
ALTER TABLE `course`
  ADD PRIMARY KEY (`Course_number`);

--
-- Indexes for table `enrollment`
--
ALTER TABLE `enrollment`
  ADD PRIMARY KEY (`EnrollmentID`),
  ADD KEY `fk_enrollment_userID` (`UserID`),
  ADD KEY `fk_enrollment_courseNumber` (`CourseNumber`);

--
-- Indexes for table `gradereport`
--
ALTER TABLE `gradereport`
  ADD PRIMARY KEY (`GradeID`),
  ADD KEY `fk_gradeReport_userID` (`UserID`),
  ADD KEY `fk_gradeReport_courseNumber` (`CourseNumber`);

--
-- Indexes for table `prerequisite`
--
ALTER TABLE `prerequisite`
  ADD PRIMARY KEY (`PrerequisiteID`),
  ADD KEY `fk_prerequisite_courseNumber` (`Course_number`),
  ADD KEY `fk_prerequisite_prerequisiteCourseNumber` (`PrerequisiteCourse_number`);

--
-- Indexes for table `room`
--
ALTER TABLE `room`
  ADD PRIMARY KEY (`RoomNumber`);

--
-- Indexes for table `schedule`
--
ALTER TABLE `schedule`
  ADD PRIMARY KEY (`ScheduleID`),
  ADD KEY `fk_schedule_courseNumber` (`CourseNumber`),
  ADD KEY `fk_schedule_roomNumber` (`RoomNumber`),
  ADD KEY `fk_schedule_teacherID` (`TeacherID`);

--
-- Indexes for table `section`
--
ALTER TABLE `section`
  ADD PRIMARY KEY (`Section_identifier`),
  ADD KEY `fk_section_courseNumber` (`Course_number`);

--
-- Indexes for table `teacherlogs`
--
ALTER TABLE `teacherlogs`
  ADD PRIMARY KEY (`log_id`),
  ADD UNIQUE KEY `teacherIDIndex` (`teacher_ID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserID`),
  ADD UNIQUE KEY `Email` (`Email`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `enrollment`
--
ALTER TABLE `enrollment`
  MODIFY `EnrollmentID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `gradereport`
--
ALTER TABLE `gradereport`
  MODIFY `GradeID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `prerequisite`
--
ALTER TABLE `prerequisite`
  MODIFY `PrerequisiteID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `schedule`
--
ALTER TABLE `schedule`
  MODIFY `ScheduleID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `teacherlogs`
--
ALTER TABLE `teacherlogs`
  MODIFY `log_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `UserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1337388;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `enrollment`
--
ALTER TABLE `enrollment`
  ADD CONSTRAINT `fk_enrollment_courseNumber` FOREIGN KEY (`CourseNumber`) REFERENCES `course` (`Course_number`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_enrollment_userID` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`) ON DELETE CASCADE;

--
-- Constraints for table `gradereport`
--
ALTER TABLE `gradereport`
  ADD CONSTRAINT `fk_gradeReport_courseNumber` FOREIGN KEY (`CourseNumber`) REFERENCES `course` (`Course_number`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_gradeReport_userID` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`) ON DELETE CASCADE;

--
-- Constraints for table `prerequisite`
--
ALTER TABLE `prerequisite`
  ADD CONSTRAINT `fk_prerequisite_courseNumber` FOREIGN KEY (`Course_number`) REFERENCES `course` (`Course_number`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_prerequisite_prerequisiteCourseNumber` FOREIGN KEY (`PrerequisiteCourse_number`) REFERENCES `course` (`Course_number`) ON DELETE CASCADE;

--
-- Constraints for table `schedule`
--
ALTER TABLE `schedule`
  ADD CONSTRAINT `fk_schedule_courseNumber` FOREIGN KEY (`CourseNumber`) REFERENCES `course` (`Course_number`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_schedule_roomNumber` FOREIGN KEY (`RoomNumber`) REFERENCES `room` (`RoomNumber`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_schedule_teacherID` FOREIGN KEY (`TeacherID`) REFERENCES `users` (`UserID`) ON DELETE CASCADE;

--
-- Constraints for table `section`
--
ALTER TABLE `section`
  ADD CONSTRAINT `fk_section_courseNumber` FOREIGN KEY (`Course_number`) REFERENCES `course` (`Course_number`) ON DELETE CASCADE;

--
-- Constraints for table `teacherlogs`
--
ALTER TABLE `teacherlogs`
  ADD CONSTRAINT `fk_teacherlogs_teacherID` FOREIGN KEY (`teacher_ID`) REFERENCES `users` (`UserID`) ON DELETE CASCADE ON UPDATE CASCADE;
SET FOREIGN_KEY_CHECKS=1;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
