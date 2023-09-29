import './App.css';
import React, {useState} from "react";
import Button from 'react-bootstrap/Button'
import Form from 'react-bootstrap/Form'
import axios from "axios";

function App() {

  const [message, setMessage] = useState("Test");

  const handleSubmit = (e) => {

    e.preventDefault();

    const config = {
      method: 'get',
      url: 'https://localhost:50000/api/items/get-item',
      headers: {
        'Content-Type': 'text/plain',
        "Access-Control-Allow-Origin": "*",
      }
    };

    axios(config)
      .then(function (response) {
        console.log(response);
        setMessage(response.data.name);
      })
      .catch(function (error) {
        console.log(error);
        setMessage("error");
      });
  }

  return (
    <div className="App">
      <header className="App-header">
        <Form onSubmit={handleSubmit}>
          <Button variant="primary" type="submit">
            Test API
          </Button>
        </Form>
        <h1>{message}</h1>
      </header>
    </div>
  );
}

export default App;
