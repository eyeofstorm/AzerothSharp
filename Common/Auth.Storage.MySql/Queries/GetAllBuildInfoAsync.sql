SELECT
    build, 
    majorVersion, 
    minorVersion, 
    bugfixVersion, 
    hotfixVersion,
    winAuthSeed,
    win64AuthSeed,
    mac64AuthSeed,
    winChecksumSeed, 
    macChecksumSeed 
FROM 
    build_info
ORDER BY
    build ASC
