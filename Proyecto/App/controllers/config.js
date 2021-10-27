const configdb = {
    user: 'admin',
    password: 'tp-iaew-2021',
    server: 'operador-logistico-db.c8f01er7irve.us-east-1.rds.amazonaws.com',
    database:'operador',
    options: {
        encrypt: true,
        trustServerCertificate: true,
    }
}

module.exports = configdb