
SELECT 
	a.id,
    a.username, 
    a.locked as lock_ip, 
    a.lock_country, 
    a.last_ip, 
    a.failed_logins, 
	ab.unbandate > UNIX_TIMESTAMP() OR ab.unbandate = ab.bandate as col6, 
    ab.unbandate = ab.bandate as col7,
    ipb.unbandate > UNIX_TIMESTAMP() OR ipb.unbandate = ipb.bandate as col8,
    ipb.unbandate = ipb.bandate as col9, 
	aa.gmlevel, 
    a.totp_secret, 
    a.salt, 
    a.verifier 
FROM 
	account a 
LEFT JOIN 
	account_access aa ON a.id = aa.id 
LEFT JOIN 
	account_banned ab ON ab.id = a.id AND ab.active = 1
LEFT JOIN 
	ip_banned ipb ON ipb.ip = @ip
WHERE 
	a.username = @username
