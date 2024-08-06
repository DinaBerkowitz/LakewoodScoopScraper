import React, { useEffect, useState } from 'react';
import axios from 'axios';

const Home = () => {

    const [scoop, setScoop] = useState([]);

    useEffect(() => {
        const getScoop = async () => {
            const { data } = await axios.get('/api/lakewoodscoop/scrape');
            setScoop(data);
        }

        getScoop();
    }, []);

    return (
        <div className="container" style={{ marginTop: 80 }}>
            {!!scoop.length && <div>
                <table className='table table-bordered'>
                    <thead>
                        <tr>
                            <th>Image</th>
                            <th>Title</th>
                            <th>Description</th>
                        </tr>
                    </thead>
                    <tbody>
                        {scoop.map(s => (
                            <tr key={s.url}>
                                <td>
                                    <img src={s.image} style={{ height: 150 }} alt={s.title} className='img=thumbnail'></img>
                                </td>
                                <td>
                                    <a href={s.url} target='_blank'>{s.title}</a>
                                </td>
                                <td>
                                    <a href={s.url} target='_blank'>{s.description}</a>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>}
        </div>
    );
};

export default Home;