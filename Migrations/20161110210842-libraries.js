exports.up = function (db, done) {
  db.createTable('libraries', {
    id: { type: 'int', primaryKey: true, autoIncrement: true },
    access_token: 'string',
    created_on: 'datetime',
    modified_on: 'datetime'
  }, done);
  done();
};

exports.down = function (db, done) {
  db.dropTable('libraries', done);
};