import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { DownloadPDF } from "./components/DownloadPDF";

const AppRoutes = [
  // {
  //   index: true,
  //   element: <Home />
  // },
  // {
  //   path: '/counter',
  //   element: <Counter />
  // },
  // {
  //   path: '/fetch-data',
  //   element: <FetchData />
  // },
  {
    index: true,
    element: <DownloadPDF />
  }
];

export default AppRoutes;
