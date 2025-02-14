create type round_type as 
enum('Buzzer');

create type music_provider as 
enum('Spotify');

create table contest (
    contest_id uuid primary key generated always as identity,
    creator_malarkey_id uuid not null,
    contest_name varchar(200) not null,
    contest_description varchar(200),
    contest_code varchar(20) not null,
    unique(contest_code)
);

create table contest_round (
    round_id uuid primary key generated always as identity,
    round_index int not null,
    contest_id uuid not null,
    round_type rount_type not null,
    round_name varchar(200) not null,
    round_description varchar(200),
    help_info varchar(200),
    number_of_seconds int,
    point_factor numeric,
    sound_byte_id uuid,
    unique(contest_id, round_index)
);


create table sound_byte (
    sound_byte_id uuid primary key generated always as identity,
    music_provider music_provider not null,
    track_id varchar(200),
    start_at numeric,
    end_at numeric
);


