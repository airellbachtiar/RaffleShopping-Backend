import React, { useState, useEffect } from "react";
import axios from "axios";
import { Card, CardHeader, CardBody, CardFooter, Heading, Text } from '@chakra-ui/react'

function Home() {

    const [raffleEvents, setRaffleEvents] = useState([]);

    useEffect(() => {
        var config = {
            method: 'get',
            url: 'https://localhost:44304/api/raffle-events/get-raffle-event',
            headers: { "Access-Control-Allow-Origin": "*" }
        };

        axios(config)
            .then(function (response) {
                setRaffleEvents(response.data);
            })
            .catch(function (error) {
                console.log(error);
            });
    }, [])

    const displayRaffleEvents = () => {
        return (
            <>
                {
                    raffleEvents.map(raffleEvent => {
                        return (
                            <Card key={raffleEvent.id}>
                                <CardHeader>
                                    <Heading size='md'>{raffleEvent.title}</Heading>
                                </CardHeader>
                                <CardBody>
                                    <Text>This is a test</Text>
                                </CardBody>
                                <CardFooter>
                                    <Text>{raffleEvent.price}</Text>
                                    {console.log(raffleEvent)}
                                </CardFooter>
                            </Card>
                        )
                    })
                }
            </>
        )
    }

    return (
        <>
            <div className="creature-list">
                {displayRaffleEvents()}
            </div>
        </>
    )
}

export default Home;