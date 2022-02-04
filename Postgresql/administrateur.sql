SET timezone = "+00:00";

DROP TABLE IF EXISTS administrateur;

CREATE TABLE IF NOT EXISTS administrateur (
	id BIGSERIAL NOT NULL UNIQUE, 
	nom VARCHAR(255) NOT NULL,
	passe VARCHAR(255) NOT NULL,
	PRIMARY KEY (id)
);

INSERT INTO administrateur(nom,passe)
VALUES('nom','passe');

SELECT * FROM administrateur;