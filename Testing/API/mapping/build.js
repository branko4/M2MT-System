var fs = require('fs')
var Collection = require('postman-collection').Collection,
    Item = require('postman-collection').Item,
    myCollection;

console.log("Building...");

console.log(process.env.TARGET_HOST);
const TARGET_HOST = process.env.TARGET_HOST;

const MODEL_ID = "MODEL";
const GET_ID = "GET";
const MODEL_PATH = "API/InformationModel";
const TARGET_DOMAIN = TARGET_HOST ? TARGET_HOST : "http://M2MT-BE-mapping:80";

const COLLECTION_FILE_NAME = `${__dirname}/collection.json`;

myCollection = new Collection({
    "item": [{
        "id": MODEL_ID,
        "name": "Endpoint for models",
        "item": []
    }]
});

myCollection.items.one(MODEL_ID).items.add(new Item({
    "name": "Send a GET request",
    "id": `${MODEL_ID}-${GET_ID}`,
    "request": {
        "url": `${TARGET_DOMAIN}/${MODEL_PATH}`,
        "method": "GET"
    }
}));

var Event = require('postman-collection').Event,
    rawEvent = {
        listen: 'test',
        script: 'tests["response code is 200"] = responseCode.code === 200'
    },
    myEvent;

myCollection.items.one(MODEL_ID).items.one(`${MODEL_ID}-${GET_ID}`).events.add(
  new Event(rawEvent)
);

console.log("Build done✅\nSaving...");

fs.writeFileSync(COLLECTION_FILE_NAME, JSON.stringify(myCollection));
