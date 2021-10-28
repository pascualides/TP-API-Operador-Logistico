const express = require('express');

const router = express.Router();


const envio_ctrl = require('../controllers/envio.controller')

router.get('/', envio_ctrl.getEnvios);

router.get('/:id', envio_ctrl.getEnvio);

// router.delete('/:id', repartidor_ctrl.deleteRepartidor);

// router.post('/', repartidor_ctrl.createRepartidor);

// router.post('/:id', repartidor_ctrl.updateRepartidor);

router.post('/', envio_ctrl.crearEnvio)

module.exports = router;
