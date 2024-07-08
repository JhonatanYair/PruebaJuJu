for i in {1..50};
do
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "juju123#" -d master -i DBScript.sql
    if [ $? -eq 0 ]
    then
        echo "SQL Script running completed"
        break
    else
        echo "not ready yet..."
        sleep 1
    fi
done