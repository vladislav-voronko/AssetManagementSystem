import { BrowserRouter } from 'react-router-dom';
import AppRouter from './router/AppRouter.tsx'
import Navbar from '../shared/ui/Navbar/Navbar.tsx';

function App() {

  return (
    <BrowserRouter>
      <Navbar />
      <AppRouter />
    </BrowserRouter>
  )
}

export default App
