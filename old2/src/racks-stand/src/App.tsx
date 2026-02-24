import { BrowserRouter as Router, Routes, Route } from "react-router";
import { Suspense, lazy } from "react";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import AppLayout from "./layout/AppLayout";
import { ScrollToTop } from "./components/common/ScrollToTop";
import Spinner from "./components/ui/spinner/Spinner";

// Lazy imports for all pages
const NotFound = lazy(() => import("./pages/errors/NotFound"));
const Forbidden = lazy(() => import("./pages/errors/Forbidden"));
const ServerError = lazy(() => import("./pages/errors/ServerError"));
const SignIn = lazy(() => import("./pages/AuthPages/SignIn"));
const SignUp = lazy(() => import("./pages/AuthPages/SignUp"));
const UserProfiles = lazy(() => import("./pages/UserProfiles"));
const Calendar = lazy(() => import("./pages/Calendar"));
const Home = lazy(() => import("./pages/Dashboard/Home"));
const WarehousesPage = lazy(() => import("./pages/warehouses/WarehousesPage"));
const RacksPage = lazy(() => import("./pages/racks/RacksPage"));
const ShelvesPage = lazy(() => import("./pages/shelves/ShelvesPage"));
const BoxesPage = lazy(() => import("./pages/boxes/BoxesPage"));

export default function App() {
  return (
    <Router>
      <ScrollToTop />

      {/* Toast Notifications */}
      <ToastContainer
        position="bottom-right"
        autoClose={3000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="colored"
      />

      {/* Suspense fallback */}
      <Suspense fallback={<Spinner  />}>
        <Routes>

          {/* Dashboard Layout */}
          <Route element={<AppLayout />}>
            <Route index path="/" element={<Home />} />
            <Route path="/profile" element={<UserProfiles />} />
            <Route path="/calendar" element={<Calendar />} />
            <Route path="/warehouses" element={<WarehousesPage />} />
            <Route path="/racks" element={<RacksPage />} />
            <Route path="/shelves" element={<ShelvesPage />} />
            <Route path="/boxes" element={<BoxesPage />} />
          </Route>

          {/* Auth Layout */}
          <Route path="/signin" element={<SignIn />} />
          <Route path="/signup" element={<SignUp />} />

          {/* Error Pages */}
          <Route path="/forbidden" element={<Forbidden />} />
          <Route path="/server-error" element={<ServerError />} />

          {/* Fallback Route */}
          <Route path="*" element={<NotFound />} />
        </Routes>
      </Suspense>
    </Router>
  );
}
