--
-- PostgreSQL database dump
--

-- Dumped from database version 10.16
-- Dumped by pg_dump version 10.16

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


--
-- Name: klient_name(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.klient_name() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
Begin
If NEW.name IS NULL THEN
RAISE EXCEPTION 'ERROR NO NAME COLUMN';
End if;
Return new;
End;
$$;


ALTER FUNCTION public.klient_name() OWNER TO postgres;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: akty_rabot; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.akty_rabot (
    klienty_kl_id integer NOT NULL,
    uslugi_us_id integer NOT NULL,
    ak_id integer NOT NULL,
    ak_ustanovka_kotla character varying(50) NOT NULL,
    ak_cena character varying(50) NOT NULL,
    ak_data date NOT NULL
);


ALTER TABLE public.akty_rabot OWNER TO postgres;

--
-- Name: klienti; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.klienti (
    kl_id integer NOT NULL,
    kl_familia character varying(100) NOT NULL,
    kl_otchestvo character varying(100),
    kl_name character varying(100) NOT NULL,
    kl_nomer_zakaza integer NOT NULL
);


ALTER TABLE public.klienti OWNER TO postgres;

--
-- Name: about_akty; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.about_akty AS
 SELECT akty_rabot.ak_id AS "Номер",
    concat_ws(' '::text, klienti.kl_familia, klienti.kl_name, klienti.kl_otchestvo) AS "Клиент",
    akty_rabot.uslugi_us_id AS "Услуга",
    akty_rabot.ak_ustanovka_kotla AS "Установка_котла",
    akty_rabot.ak_cena AS "Цена",
    akty_rabot.ak_data AS "Дата"
   FROM (public.akty_rabot
     JOIN public.klienti ON ((akty_rabot.klienty_kl_id = klienti.kl_id)));


ALTER TABLE public.about_akty OWNER TO postgres;

--
-- Name: about_klient; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.about_klient AS
 SELECT klienti.kl_id AS "Номер",
    klienti.kl_familia AS "Фамилия",
    klienti.kl_name AS "Имя",
    klienti.kl_otchestvo AS "Отчество",
    klienti.kl_nomer_zakaza AS "Номер_заказа"
   FROM public.klienti;


ALTER TABLE public.about_klient OWNER TO postgres;

--
-- Name: sotrudniki; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.sotrudniki (
    st_id integer NOT NULL,
    st_familia character varying(100) NOT NULL,
    st_otchestvo character varying(100),
    st_imya character varying(100) NOT NULL,
    st_doljnost character varying(50) NOT NULL,
    st_address character varying(50) NOT NULL,
    st_zarplata character varying(50) NOT NULL
);


ALTER TABLE public.sotrudniki OWNER TO postgres;

--
-- Name: about_sotrud; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.about_sotrud AS
 SELECT sotrudniki.st_id AS "Номер",
    sotrudniki.st_familia AS "Фамилия",
    sotrudniki.st_imya AS "Имя",
    sotrudniki.st_otchestvo AS "Отчество",
    sotrudniki.st_doljnost AS "Должность",
    sotrudniki.st_address AS "Адрес",
    sotrudniki.st_zarplata AS "Зарплата"
   FROM public.sotrudniki;


ALTER TABLE public.about_sotrud OWNER TO postgres;

--
-- Name: uslugi; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.uslugi (
    sotrudniki_st_id integer,
    us_id integer NOT NULL,
    razrabotkahertejei character varying(50) NOT NULL,
    vidahazaklyhenii character varying(50) NOT NULL,
    texniheskiinadzor character varying(50) NOT NULL,
    texniheskayaekspertiza character varying(50) NOT NULL
);


ALTER TABLE public.uslugi OWNER TO postgres;

--
-- Name: about_uslugi; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.about_uslugi AS
 SELECT concat_ws(' '::text, sotrudniki.st_familia, sotrudniki.st_imya, sotrudniki.st_otchestvo) AS "ФИО_сотрудника",
    uslugi.us_id AS "Номер",
    uslugi.razrabotkahertejei AS "Разработка_чертежей",
    uslugi.vidahazaklyhenii AS "Выдача_заключений",
    uslugi.texniheskiinadzor AS "Технический_надзор",
    uslugi.texniheskayaekspertiza AS "Техническая_экспертиза"
   FROM (public.uslugi
     JOIN public.sotrudniki ON ((uslugi.sotrudniki_st_id = sotrudniki.st_id)));


ALTER TABLE public.about_uslugi OWNER TO postgres;

--
-- Name: administracia; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.administracia (
    surname character varying(30),
    name character varying(20),
    otchestvo character varying(25),
    login character varying(25),
    password character varying(25)
);


ALTER TABLE public.administracia OWNER TO postgres;

--
-- Data for Name: administracia; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.administracia (surname, name, otchestvo, login, password) FROM stdin;
Иванов	Сергей	Александрович	admin	admin
\.


--
-- Data for Name: akty_rabot; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.akty_rabot (klienty_kl_id, uslugi_us_id, ak_id, ak_ustanovka_kotla, ak_cena, ak_data) FROM stdin;
1	1	1	Установлен	32400	2021-05-21
2	2	2	Установлен	90000	2021-05-15
3	3	3	Установлен	150000	2021-06-16
4	4	4	Установлен	200000	2021-06-17
5	5	5	Установлен	200000	2021-06-25
\.


--
-- Data for Name: klienti; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.klienti (kl_id, kl_familia, kl_otchestvo, kl_name, kl_nomer_zakaza) FROM stdin;
2	Сергеев	Сергеевич	Сергей	444121
3	Павлов	Сергеевич	Артём	565783
4	Бакин	Семёнович	Семён	666767
5	Серов	Евгеньевич	Пётр	232001
1	Пупкин	Георгиевич	Георг	323229
\.


--
-- Data for Name: sotrudniki; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.sotrudniki (st_id, st_familia, st_otchestvo, st_imya, st_doljnost, st_address, st_zarplata) FROM stdin;
1	Зубенко	Игоревич	Даниил	Директор	Ростов	43200
2	Чернышова	Татьяна	Геннадьевна	Уборщица	Новочипецк	12200
4	Чернов	Александр	Витальевич	Бухгалтер	Ростов	10000
5	Щукин	Егор	Александрович	Инженер-слесарь	Ростов	30000
3	Погорельский	Сергеевич	Павел	Инженер	Ростов	50000
\.


--
-- Data for Name: uslugi; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.uslugi (sotrudniki_st_id, us_id, razrabotkahertejei, vidahazaklyhenii, texniheskiinadzor, texniheskayaekspertiza) FROM stdin;
1	1	БКЗ397539ФБ	Выдано	Проведён	Пройдена
2	2	ВК7697459ВД	Выдано	Проведён	Пройдена
3	3	СБ7611229РЕ	Выдано	Проведён	Пройдена
4	4	ЖЖ7321779МБ	Выдано	Проведён	Пройдена
5	5	ЧС7354219ТИ	Выдано	Проведён	Пройдена
\.


--
-- Name: akty_rabot akty_rabot_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.akty_rabot
    ADD CONSTRAINT akty_rabot_pk PRIMARY KEY (ak_id);


--
-- Name: klienti klienty_kl_nomer_zakaza_un; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.klienti
    ADD CONSTRAINT klienty_kl_nomer_zakaza_un UNIQUE (kl_nomer_zakaza);


--
-- Name: klienti klienty_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.klienti
    ADD CONSTRAINT klienty_pk PRIMARY KEY (kl_id);


--
-- Name: sotrudniki sotrudniki_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sotrudniki
    ADD CONSTRAINT sotrudniki_pk PRIMARY KEY (st_id);


--
-- Name: uslugi uslugi_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.uslugi
    ADD CONSTRAINT uslugi_pk PRIMARY KEY (us_id);


--
-- Name: klienti klient_name; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER klient_name BEFORE INSERT OR UPDATE ON public.klienti FOR EACH ROW EXECUTE PROCEDURE public.klient_name();


--
-- Name: akty_rabot akty_rabot_klienty_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.akty_rabot
    ADD CONSTRAINT akty_rabot_klienty_fk FOREIGN KEY (klienty_kl_id) REFERENCES public.klienti(kl_id);


--
-- Name: akty_rabot akty_rabot_uslugi_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.akty_rabot
    ADD CONSTRAINT akty_rabot_uslugi_fk FOREIGN KEY (uslugi_us_id) REFERENCES public.uslugi(us_id);


--
-- Name: uslugi uslugi_sotrudniki_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.uslugi
    ADD CONSTRAINT uslugi_sotrudniki_fk FOREIGN KEY (sotrudniki_st_id) REFERENCES public.sotrudniki(st_id);


--
-- PostgreSQL database dump complete
--

