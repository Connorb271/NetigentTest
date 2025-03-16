import "bootstrap/dist/css/bootstrap.min.css";
import AppProjectTable from "./AppProjectTable";
import axios from "axios";

axios.defaults.baseURL = "https://localhost:7126";

function App() {
    return (
        <div className="container-fluid">
            <div className="container mt-4">
                <div className="row justify-content-center">
                    <div className="col-12">
                        <div className="card">
                            <div className="card-body">
                                <AppProjectTable />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default App;
