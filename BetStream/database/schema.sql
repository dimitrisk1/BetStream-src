-- SQL schema for BetStream (Postgres)

CREATE TABLE IF NOT EXISTS teams (
    id serial PRIMARY KEY,
    name varchar(200) NOT NULL UNIQUE,
    created_at timestamptz NOT NULL DEFAULT now()
);

CREATE TABLE IF NOT EXISTS matches (
    id uuid PRIMARY KEY,
    home_team_id int NOT NULL REFERENCES teams(id) ON DELETE RESTRICT,
    away_team_id int NOT NULL REFERENCES teams(id) ON DELETE RESTRICT,
    home_odds numeric(18,4) NOT NULL,
    away_odds numeric(18,4) NOT NULL,
    start_time timestamptz NOT NULL,
    created_at timestamptz NOT NULL DEFAULT now()
);

CREATE TABLE IF NOT EXISTS message_events (
    id bigserial PRIMARY KEY,
    topic varchar(200) NOT NULL,
    key varchar(200),
    payload text NOT NULL,
    received_at timestamptz NOT NULL DEFAULT now(),
    partition varchar(50),
    offset bigint
);

-- Indexes for common queries
CREATE INDEX IF NOT EXISTS idx_messageevents_received_at ON message_events(received_at);

