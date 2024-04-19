import type process from 'process';

const DEV_CONFIG = {
    BASE_URL: '',
    API_URL: "https://localhost:7145"
};

const PROD_CONFIG = {
    BASE_URL: '/replay/',
    API_URL: "http://testing.canne.tv/replay/api"
};


export default process.env.NODE_ENV === "development" ? DEV_CONFIG : PROD_CONFIG;