use raven;

create table otp_data
(
	user_id varchar(50),
    otp_code varchar(50),
    otp_status enum('USED','UNUSED','EXPIRED'),
    number_of_tries tinyint,
    generated_at datetime,
    expires_at datetime
);