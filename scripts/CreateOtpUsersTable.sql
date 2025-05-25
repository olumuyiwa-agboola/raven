use raven;

create table otp_users
(
	user_id varchar(50),
    first_name varchar(100),
    last_name varchar(100),
    email_address varchar(200),
    phone_number varchar(50),
    created_at datetime,
    last_updated_at datetime
);