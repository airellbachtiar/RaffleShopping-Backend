import http from 'k6/http';
import { check, sleep } from 'k6';

import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

export const options = {
  stages: [
    { duration: "5s", target: 5 },
    { duration: "10s", target: 100 },
    { duration: "10s", target: 1000 },
    { duration: "10s", target: 1000 },
  ],
};

export default function () {
  const url = 'http://raffleshopping.com/api/catalogs';
  // const url = 'http://raffleshopping.com/api/raffle-events/get-raffle-event';
  const headers = {
    'Content-Type': 'application/json'
  };

  const res = http.get(url, { headers: headers });

  // Simulate user think time (pause between requests)
  sleep(Math.random() * 3); // Sleep for a random duration (up to 3 seconds)

  // Add checks based on the response
  check(res, {
    'status is 200': (r) => r.status === 200,
  });
}

// export function handleSummary(data) {
//   return {
//     'TestSummaryReport.html': htmlReport(data, { debug: true })
//   };
// }