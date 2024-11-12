import express from "express";
import homepageController from "../controllers/homepageController";

let router = express.Router();

let initWebRoutes = (app)=> {
    router.get("/", homepageController.getHomePage); //done
    router.get("/webhook", homepageController.getWebhook); //done
    router.post("/webhook", homepageController.postWebhook);
    router.post("/set-up-profile", homepageController.handleSetupProfile);
    router.get("/set-up-profile", homepageController.getSetupProfilePage);

    router.get("/info-order", homepageController.getInfoOrderPage);
    router.post("/set-info-order", homepageController.setInfoOrder);
    return app.use("/", router);
};

module.exports = initWebRoutes;
