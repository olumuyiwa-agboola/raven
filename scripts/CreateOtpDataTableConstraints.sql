use raven;

alter table otp_data
add primary key (user_id),
add index otp_status_index (otp_status);