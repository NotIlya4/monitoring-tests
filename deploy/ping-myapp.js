import http from 'k6/http';
import { sleep } from 'k6';

export default function () {
  http.get('http://localhost:5000/weather?city=london&days=10000');
  sleep((Math.random() * 0.25) * 2);
} 