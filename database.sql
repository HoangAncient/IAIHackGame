-- phpMyAdmin SQL Dump
-- version 6.0.0-dev+20230213.ef941c2080
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th7 15, 2023 lúc 06:02 PM
-- Phiên bản máy phục vụ: 10.4.24-MariaDB
-- Phiên bản PHP: 8.1.5

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `unitygame`
--
CREATE DATABASE unitygame;
USE unitygame;
-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `banks`
--

CREATE TABLE `banks` (
  `BankID` int(11) NOT NULL,
  `BankName` varchar(255) DEFAULT NULL,
  `DayCreated` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `formfill`
--

CREATE TABLE `formfill` (
  `QuestionID` int(11) DEFAULT NULL,
  `choice` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `mc2`
--

CREATE TABLE `mc2` (
  `QuestionID` int(11) DEFAULT NULL,
  `choice1` text DEFAULT NULL,
  `choice2` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `mc2`
--

INSERT INTO `mc2` (`QuestionID`, `choice1`, `choice2`) VALUES
(1, 'Đẹp', 'Rất đẹp'),
(3, 'Hehe', 'Haha'),
(2, 'Dung', 'Sai');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `mc3`
--

CREATE TABLE `mc3` (
  `QuestionID` int(11) DEFAULT NULL,
  `choice1` text DEFAULT NULL,
  `choice2` text DEFAULT NULL,
  `choice3` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `mc3`
--

INSERT INTO `mc3` (`QuestionID`, `choice1`, `choice2`, `choice3`) VALUES
(2, 'Dung', 'Sai', 'K dung');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `mc4`
--

CREATE TABLE `mc4` (
  `QuestionID` int(11) DEFAULT NULL,
  `choice1` text DEFAULT NULL,
  `choice2` text DEFAULT NULL,
  `choice3` text DEFAULT NULL,
  `choice4` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `persons`
--

CREATE TABLE `persons` (
  `personID` int(11) NOT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `Birthday` date DEFAULT NULL,
  `username` varchar(50) DEFAULT NULL,
  `password` varchar(50) DEFAULT NULL,
  `job` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `persons`
--

INSERT INTO `persons` (`personID`, `FirstName`, `LastName`, `Birthday`, `username`, `password`, `job`) VALUES
(1, 'Hoang', 'Dinh', '0000-00-00', 'hoangdinhnho23', 'kaka', 'student'),
(2, 'Tao', 'Loi', '2023-07-10', 'hello', 'haha', 'Teacher'),
(3, 'pp', 'pp', NULL, 'hehe', 'haha', 'student');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `questinquiz`
--

CREATE TABLE `questinquiz` (
  `QuizID` int(11) NOT NULL,
  `QuestionID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `questions`
--

CREATE TABLE `questions` (
  `QuestionID` int(11) NOT NULL,
  `Question` text DEFAULT NULL,
  `FilePath` text DEFAULT NULL,
  `answer` text DEFAULT NULL,
  `QuestionType` varchar(50) DEFAULT NULL,
  `Chapter` int(11) NOT NULL,
  `Hardmode` int(11) NOT NULL,
  `BankID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `questions`
--

INSERT INTO `questions` (`QuestionID`, `Question`, `FilePath`, `answer`, `QuestionType`, `Chapter`, `Hardmode`, `BankID`) VALUES
(1, 'Hoàng có đẹp trai k?', NULL, 'Rất đẹp', 'MC2', 0, 0, NULL),
(2, 'Hooang tan thu', NULL, 'Dung', 'MC2', 0, 0, NULL),
(3, 'Hehe?', NULL, 'Hehe', 'MC2', 0, 0, NULL);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `quizzes`
--

CREATE TABLE `quizzes` (
  `QuizID` int(11) NOT NULL,
  `QuizName` varchar(255) DEFAULT NULL,
  `QuizStartTime` datetime NOT NULL,
  `QuizEndTime` int(11) NOT NULL,
  `QuizType` varchar(50) DEFAULT NULL,
  `TeacherID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `studentdoquiz`
--

CREATE TABLE `studentdoquiz` (
  `DoID` int(11) NOT NULL,
  `timeStart` datetime DEFAULT NULL,
  `timeEnd` datetime DEFAULT NULL,
  `timePeriod` int(11) DEFAULT NULL,
  `grade` int(11) DEFAULT NULL,
  `rank` int(11) DEFAULT NULL,
  `studentID` int(11) DEFAULT NULL,
  `quizID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `teacheraccessbank`
--

CREATE TABLE `teacheraccessbank` (
  `TeacherID` int(11) NOT NULL,
  `BankID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `banks`
--
ALTER TABLE `banks`
  ADD PRIMARY KEY (`BankID`);

--
-- Chỉ mục cho bảng `formfill`
--
ALTER TABLE `formfill`
  ADD KEY `QuestionID` (`QuestionID`);

--
-- Chỉ mục cho bảng `mc2`
--
ALTER TABLE `mc2`
  ADD KEY `QuestionID` (`QuestionID`);

--
-- Chỉ mục cho bảng `mc3`
--
ALTER TABLE `mc3`
  ADD KEY `QuestionID` (`QuestionID`);

--
-- Chỉ mục cho bảng `mc4`
--
ALTER TABLE `mc4`
  ADD KEY `QuestionID` (`QuestionID`);

--
-- Chỉ mục cho bảng `persons`
--
ALTER TABLE `persons`
  ADD PRIMARY KEY (`personID`);

--
-- Chỉ mục cho bảng `questinquiz`
--
ALTER TABLE `questinquiz`
  ADD PRIMARY KEY (`QuizID`,`QuestionID`),
  ADD KEY `QuestionID` (`QuestionID`);

--
-- Chỉ mục cho bảng `questions`
--
ALTER TABLE `questions`
  ADD PRIMARY KEY (`QuestionID`),
  ADD KEY `BankID` (`BankID`);

--
-- Chỉ mục cho bảng `quizzes`
--
ALTER TABLE `quizzes`
  ADD PRIMARY KEY (`QuizID`),
  ADD KEY `TeacherID` (`TeacherID`);

--
-- Chỉ mục cho bảng `studentdoquiz`
--
ALTER TABLE `studentdoquiz`
  ADD PRIMARY KEY (`DoID`),
  ADD KEY `quizID` (`quizID`),
  ADD KEY `studentID` (`studentID`);

--
-- Chỉ mục cho bảng `teacheraccessbank`
--
ALTER TABLE `teacheraccessbank`
  ADD PRIMARY KEY (`TeacherID`,`BankID`),
  ADD KEY `BankID` (`BankID`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `banks`
--
ALTER TABLE `banks`
  MODIFY `BankID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `persons`
--
ALTER TABLE `persons`
  MODIFY `personID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT cho bảng `questions`
--
ALTER TABLE `questions`
  MODIFY `QuestionID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT cho bảng `quizzes`
--
ALTER TABLE `quizzes`
  MODIFY `QuizID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `studentdoquiz`
--
ALTER TABLE `studentdoquiz`
  MODIFY `DoID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `formfill`
--
ALTER TABLE `formfill`
  ADD CONSTRAINT `formfill_ibfk_1` FOREIGN KEY (`QuestionID`) REFERENCES `questions` (`QuestionID`);

--
-- Các ràng buộc cho bảng `mc2`
--
ALTER TABLE `mc2`
  ADD CONSTRAINT `mc2_ibfk_1` FOREIGN KEY (`QuestionID`) REFERENCES `questions` (`QuestionID`);

--
-- Các ràng buộc cho bảng `mc3`
--
ALTER TABLE `mc3`
  ADD CONSTRAINT `mc3_ibfk_1` FOREIGN KEY (`QuestionID`) REFERENCES `questions` (`QuestionID`);

--
-- Các ràng buộc cho bảng `mc4`
--
ALTER TABLE `mc4`
  ADD CONSTRAINT `mc4_ibfk_1` FOREIGN KEY (`QuestionID`) REFERENCES `questions` (`QuestionID`);

--
-- Các ràng buộc cho bảng `questinquiz`
--
ALTER TABLE `questinquiz`
  ADD CONSTRAINT `questinquiz_ibfk_1` FOREIGN KEY (`QuizID`) REFERENCES `quizzes` (`QuizID`),
  ADD CONSTRAINT `questinquiz_ibfk_2` FOREIGN KEY (`QuestionID`) REFERENCES `questions` (`QuestionID`);

--
-- Các ràng buộc cho bảng `questions`
--
ALTER TABLE `questions`
  ADD CONSTRAINT `questions_ibfk_1` FOREIGN KEY (`BankID`) REFERENCES `banks` (`BankID`);

--
-- Các ràng buộc cho bảng `quizzes`
--
ALTER TABLE `quizzes`
  ADD CONSTRAINT `quizzes_ibfk_1` FOREIGN KEY (`TeacherID`) REFERENCES `persons` (`personID`);

--
-- Các ràng buộc cho bảng `studentdoquiz`
--
ALTER TABLE `studentdoquiz`
  ADD CONSTRAINT `studentdoquiz_ibfk_1` FOREIGN KEY (`quizID`) REFERENCES `quizzes` (`QuizID`),
  ADD CONSTRAINT `studentdoquiz_ibfk_2` FOREIGN KEY (`studentID`) REFERENCES `persons` (`personID`);

--
-- Các ràng buộc cho bảng `teacheraccessbank`
--
ALTER TABLE `teacheraccessbank`
  ADD CONSTRAINT `teacheraccessbank_ibfk_1` FOREIGN KEY (`TeacherID`) REFERENCES `persons` (`personID`),
  ADD CONSTRAINT `teacheraccessbank_ibfk_2` FOREIGN KEY (`BankID`) REFERENCES `banks` (`BankID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
