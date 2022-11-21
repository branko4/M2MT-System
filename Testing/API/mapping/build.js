var fs = require('fs')
var Header = require('postman-collection').Header
var RequestBody = require('postman-collection').RequestBody
var Collection = require('postman-collection').Collection,
    Item = require('postman-collection').Item,
    myCollection;

console.log("Building...");

console.log(process.env.TARGET_HOST);
const TARGET_HOST = process.env.TARGET_HOST;

const MODEL_ID = "MODEL";
const GET_ID = "GET";
const POST_ID = "POST";
const MODEL_PATH = "API/InformationModel";
const TARGET_DOMAIN = TARGET_HOST ? TARGET_HOST : "http://M2MT-BE-mapping:80";

const MAPPING_ID = "MAPPING";
const MAPPING_PATH = "API/Mapping"


const COLLECTION_FILE_NAME = `${__dirname}/collection.json`;

var Event = require('postman-collection').Event,
    rawEvent = {
        listen: 'test',
        script: 'tests["response code is 200"] = responseCode.code === 200'
    },
    myEvent;

myCollection = new Collection({
    "item": [
      {
        "id": MODEL_ID,
        "name": "Endpoint for models",
        "item": []
      },
      {
        "id": MAPPING_ID,
        "name": "Endpoint for mappings",
        "item": [
          new Item({
            "name": `Send a GET request ${MAPPING_ID}-${GET_ID}`,
            "id": `${MAPPING_ID}-${GET_ID}`,
            "request": {
              "url": `${TARGET_DOMAIN}/${MAPPING_PATH}`,
              "method": "GET"
            },
          }),
          new Item({
            "name": `Send a POST request ${MAPPING_ID}-${POST_ID}`,
            "id": `${MAPPING_ID}-${POST_ID}`,
            "request": {
              "url": `${TARGET_DOMAIN}/${MAPPING_PATH}`,
              "method": "POST",
              // TODO FIXME 400 bad request error
              // "body": new RequestBody({
              //   raw: `{
              //     "ID":"41c652fd-c810-461f-97d6-9cfd2a4e0aec",
              //     "modelFrom":"ea6f6bab-bf17-4fcb-addd-eb77614a0272",
              //     "modelTo":"b8ad4ffe-9648-451d-a515-bbb0967573c3"
              //   }`
              // }),
              "body": new RequestBody({
                raw: "{ \"ID\":\"41c652fd-c810-461f-97d6-9cfd2a4e0aec\", \"modelFrom\":\"ea6f6bab-bf17-4fcb-addd-eb77614a0272\", \"modelTo\":\"b8ad4ffe-9648-451d-a515-bbb0967573c3\" }"
              }),
              "header": [
                new Header({
                        key: 'Content-Type',
                        value: 'application/json'
                }),
                new Header({
                        key: 'Transfer-Encoding',
                        value: 'chunked'
                })
              ]
            },
          }),
        ]
      },
    ]
});

// myCollection.items.one(MAPPING_ID).items.one(`${MAPPING_ID}-${POST_ID}`).header.add(new Header({
//         key: 'Content-Type',
//         value: 'application/json'
// }));

myCollection.items.one(MODEL_ID).items.add(new Item({
    "name": `Send a GET request ${MODEL_ID}-${GET_ID}`,
    "id": `${MODEL_ID}-${GET_ID}`,
    "request": {
        "url": `${TARGET_DOMAIN}/${MODEL_PATH}`,
        "method": "GET"
    }
}));



myCollection.items.one(MODEL_ID).items.one(`${MODEL_ID}-${GET_ID}`).events.add(
  new Event(rawEvent)
);
myCollection.items.one(MAPPING_ID).items.one(`${MAPPING_ID}-${GET_ID}`).events.add(
  new Event(rawEvent)
);
myCollection.items.one(MAPPING_ID).items.one(`${MAPPING_ID}-${POST_ID}`).events.add(
  new Event(rawEvent)
);

console.log("Build done✅\nSaving...");

fs.writeFileSync(COLLECTION_FILE_NAME, JSON.stringify(myCollection));
