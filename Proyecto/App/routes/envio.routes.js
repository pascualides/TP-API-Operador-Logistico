const express = require('express');

const router = express.Router();


const envio_ctrl = require('../controllers/envio.controller')

router.get('/', envio_ctrl.getEnvios);

router.get('/:id', envio_ctrl.getEnvio);

router.post('/:id/repartidor', repartidor_ctrl.asignarRepartidor);

router.post('/:id/entrega', repartidor_ctrl.setEntrega);

router.post('/', envio_ctrl.crearEnvio)

module.exports = router;
