DROP SCHEMA IF EXISTS goodreads_v2 CASCADE;
CREATE SCHEMA goodreads_v2;

SET SCHEMA 'goodreads_v2';

CREATE TABLE binding_type (
    id SMALLINT PRIMARY KEY,
    type VARCHAR(50)
);

CREATE TABLE publisher (
    id SMALLINT PRIMARY KEY,
    publisher_name VARCHAR(50)
);

CREATE TABLE author (
    id SMALLINT PRIMARY KEY,
    first_name VARCHAR(50),
    middle_name VARCHAR(250),
    last_name VARCHAR(50)
);

CREATE TABLE genre(
    id SMALLINT PRIMARY KEY ,
    genre VARCHAR(50)
);

CREATE TABLE book (
    id INTEGER PRIMARY KEY,
    isbn VARCHAR(15),
    title VARCHAR(200),
    page_count SMALLINT,
    year_published SMALLINT,
    binding_id SMALLINT REFERENCES binding_type(id),
    publisher_id SMALLINT REFERENCES publisher(id),
    author_id SMALLINT REFERENCES author(id)
);

CREATE TABLE co_authors(
    book_id INTEGER,
    author_id SMALLINT,
    PRIMARY KEY (book_id, author_id),
    FOREIGN KEY (book_id) REFERENCES book(id),
    FOREIGN KEY (author_id) REFERENCES author(id)
);

CREATE TABLE book_genre (
    genre_id SMALLINT,
    book_id INTEGER,
    PRIMARY KEY (genre_id, book_id),
    FOREIGN KEY (genre_id) REFERENCES genre(id),
    FOREIGN KEY (book_id) REFERENCES book(id)
);

CREATE TABLE profile(
    id SMALLINT Primary Key ,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    profile_name VARCHAR(50)
);

CREATE TABLE book_read(
    profile_id SMALLINT,
    book_id INTEGER,
    rating SMALLINT,
    date_started DATE,
    date_finished DATE,
    status VARCHAR(7),
    PRIMARY KEY (profile_id, book_id),
    FOREIGN KEY (profile_id) REFERENCES profile(id),
    FOREIGN KEY (book_id) REFERENCES book(id)
);