-- --------------------------------------------------------
--
-- Création de la bdd "BabyParty"
-- Vérifier que la "Séquence" de cette table a été générée
--
-- --------------------------------------------------------

CREATE DATABASE "BabyParty"
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'French_France.1252'
    LC_CTYPE = 'French_France.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

-- --------------------------------------------------------
--
-- Paramètres
--
-- --------------------------------------------------------

SET timezone = "+00:00";

-- --------------------------------------------------------
--
-- Structure de la table "rencontre"
--
-- --------------------------------------------------------

DROP TABLE IF EXISTS rencontre;

CREATE TABLE IF NOT EXISTS rencontre (
	id BIGSERIAL NOT NULL UNIQUE, 
	date_rencontre DATE NOT NULL UNIQUE,
	equipe1 VARCHAR(255) NOT NULL,
	equipe2 VARCHAR(255) NOT NULL,
	score1 SMALLINT, -- peut être null pour rendre possible un calendrier prévisionnel
	score2 SMALLINT, -- idem
	PRIMARY KEY (id)
);

-- --------------------------------------------------------
--
-- Ajout de données
--
-- --------------------------------------------------------

INSERT INTO rencontre(date_rencontre, equipe1, equipe2, score1, score2)
VALUES('2022-02-01', 'Losc', 'Paris', 10, 4);

INSERT INTO rencontre(date_rencontre, equipe1, equipe2, score1, score2)
VALUES('2022-02-02', 'Totos', 'Doudoux', 1, 2);

INSERT INTO rencontre(date_rencontre, equipe1, equipe2)
VALUES('2022-01-31', 'é1', 'à2'); -- les scores sont "null" en attente de données par la suite

-- --------------------------------------------------------
--
-- Vérification
--
-- --------------------------------------------------------

SELECT * FROM rencontre;