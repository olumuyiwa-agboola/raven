use raven;

alter table otp_users
add primary key (user_id),
modify column email_address varchar(200) unique not null,
modify column phone_number varchar(50) unique not null,
add index email_address_index (email_address),
add index phone_number_index (phone_number);