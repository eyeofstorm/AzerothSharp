
UPDATE 
	account 
SET 
	session_key = @sessionKey, 
    last_ip = @lastIp, 
    last_login = NOW(), 
    failed_logins = 0, 
    os = @operatingSystem 
WHERE 
	username = @userName
