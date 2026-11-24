-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2026. Nov 24. 20:11
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `neoton_familia`
--
CREATE DATABASE IF NOT EXISTS `neoton_familia` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE `neoton_familia`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `albumok`
--

CREATE TABLE `albumok` (
  `id` int(11) NOT NULL,
  `cim` varchar(150) NOT NULL,
  `ev` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `albumok`
--

INSERT INTO `albumok` (`id`, `cim`, `ev`) VALUES
(1, 'Bolond város', 1971),
(2, 'Menedékház', 1976),
(3, 'Csak a zene', 1977),
(4, 'Napraforgó', 1979),
(5, 'Marathon', 1980),
(6, 'A Família', 1981),
(7, 'Szerencsejáték', 1982),
(8, 'Neoton Família VII.', 1983),
(9, 'Karnevál', 1984),
(10, 'Magánügyek', 1985),
(11, 'Minek ez a cirkusz?', 1986),
(12, 'Védőháló nélkül', 1987),
(13, 'Vonalra várva', 1988),
(14, 'Abrakadabra', 1989);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `koncertek`
--

CREATE TABLE `koncertek` (
  `id` int(11) NOT NULL,
  `datum` date NOT NULL,
  `helyszin` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `koncertek`
--

INSERT INTO `koncertek` (`id`, `datum`, `helyszin`) VALUES
(1, '2026-10-24', 'Budapest'),
(2, '2026-11-07', 'Miskolc'),
(3, '2026-11-21', 'Debrecen'),
(4, '2026-12-05', 'Győr'),
(5, '2026-12-19', 'Szeged'),
(6, '2027-01-16', 'Pécs'),
(7, '2027-02-06', 'Nyíregyháza'),
(8, '2027-02-20', 'Eger'),
(9, '2027-03-13', 'Kecskemét'),
(10, '2027-04-03', 'Szolnok');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `megtekintesek`
--

CREATE TABLE `megtekintesek` (
  `id` int(11) NOT NULL,
  `album_id` int(11) NOT NULL,
  `darab` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `megtekintesek`
--

INSERT INTO `megtekintesek` (`id`, `album_id`, `darab`) VALUES
(1, 1, 7754),
(2, 2, NULL),
(3, 3, 3116),
(4, 4, NULL),
(5, 5, NULL),
(6, 6, NULL),
(7, 7, 25729),
(8, 8, NULL),
(9, 9, NULL),
(10, 10, 13523),
(11, 11, 17395),
(12, 12, 5872),
(13, 13, 11352),
(14, 14, 8317);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `albumok`
--
ALTER TABLE `albumok`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `koncertek`
--
ALTER TABLE `koncertek`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `megtekintesek`
--
ALTER TABLE `megtekintesek`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `uq_megtekintesek_album_id` (`album_id`),
  ADD UNIQUE KEY `album_id` (`album_id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `albumok`
--
ALTER TABLE `albumok`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT a táblához `koncertek`
--
ALTER TABLE `koncertek`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT a táblához `megtekintesek`
--
ALTER TABLE `megtekintesek`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `megtekintesek`
--
ALTER TABLE `megtekintesek`
  ADD CONSTRAINT `fk_megtekintesek_albumok` FOREIGN KEY (`album_id`) REFERENCES `albumok` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
