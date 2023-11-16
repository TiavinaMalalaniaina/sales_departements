import { Routes, Route } from 'react-router-dom';
import SideBar from './templates/sidebars/SideBar';
import Dept from './views/Dept';

function App() {
    return (
        <Routes>
            <Route path='/' element={<Dept/>}/>
        </Routes>
    );
}

export default App;


