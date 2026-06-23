import { BrowserRouter } from 'react-router-dom';
import AppRouter from './router/index.tsx'
import Navbar from './components/UI/Navbar/Navbar.tsx';

function App() {

  return (
    <BrowserRouter>
      <Navbar />
      <AppRouter />
    </BrowserRouter>
  )
}

export default App
